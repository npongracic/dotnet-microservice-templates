using BuildingBlocks.EventBus;
using BuildingBlocks.EventBus.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using NSwag;
using NSwag.Generation.Processors.Security;
using SC.API.CleanArchitecture.API.Filters;
using SC.API.CleanArchitecture.API.ModelBinders;
using SC.API.CleanArchitecture.API.Security;
using SC.API.CleanArchitecture.API.Services;

using SC.API.CleanArchitecture.Application;
using SC.API.CleanArchitecture.Application.Common;
using SC.API.CleanArchitecture.Application.Common.Interfaces;
using SC.API.CleanArchitecture.Infrastructure;
using SC.API.CleanArchitecture.Infrastructure.Identity;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.API
{
    public class Startup
    {
        private readonly IWebHostEnvironment _currentEnvironment;
        private readonly IConfigurationRoot _configuration;

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();

            _configuration = builder.Build();
            _currentEnvironment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var multipartBodyLengthLimit = _configuration.GetValue<long>("MultipartBodyLengthLimit");

            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = multipartBodyLengthLimit;
            });

            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = (int)multipartBodyLengthLimit;
                x.MultipartBodyLengthLimit = multipartBodyLengthLimit; 
                x.MultipartHeadersLengthLimit = (int)multipartBodyLengthLimit;
            });

            services.AddOptions();

            services.Configure<TokenSettings>(t => _configuration.GetSection(nameof(TokenSettings)).Bind(t));

            services.AddHealthChecks();            

            services.AddCors();

            services.AddApplication(_configuration);
            services.AddInfrastructure(_configuration, _currentEnvironment.ContentRootPath);
            services.AddMediatR(Assembly.GetExecutingAssembly());

            //Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true; 

            var tokenSettings = _configuration.GetSection(nameof(TokenSettings)).Get<TokenSettings>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.Authority = tokenSettings.Authority;
                    options.Audience = tokenSettings.Audience;
                    options.IncludeErrorDetails = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        //ValidAudiences = new[] { "master-realm", "account" },
                        ValidateIssuer = true,
                        ValidIssuer = tokenSettings.Authority,
                        ValidateLifetime = false
                    };
                });


            services.AddAuthorization(options => {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser()
                    .Build();
            });

            services.AddHttpContextAccessor();

            services.AddScoped<IClaimsTransformation, AddPartyInfoClaimsTransformation>();
            services.AddTransient<ICurrentUserService, CurrentUserService>();

            services.AddWebEncoders();

            services
                .AddControllersWithViews(options => options.Filters.Add(new ApiExceptionFilter(_currentEnvironment)))
                .AddDapr()
                .ConfigureApiBehaviorOptions(options => {
                    options.SuppressModelStateInvalidFilter = false;
                    options.InvalidModelStateResponseFactory = c => {
                        return new BadRequestObjectResult(c.ModelState);
                    };
                });

            services.AddRouting(co => {
                co.LowercaseUrls = true;
            });

            services.AddLocalization(o => o.ResourcesPath = "Resources");
            services.AddWebEncoders();

            services.Configure<Microsoft.AspNetCore.ResponseCompression.GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Fastest);

            services.AddResponseCompression(options => {
                options.Providers.Add<Microsoft.AspNetCore.ResponseCompression.GzipCompressionProvider>();
            });


            services.AddOpenApiDocument(configure => {
                configure.Title = "SC.API.CleanArchitecture.Web";
                configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });

                configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });

            services.AddSingleton<OperationsMapper>();
            services.AddScoped<IAuthorizationHandler, OrganizationPermissionRequirementAuthorizationHandler>();

            services.AddGrpc(options =>
            {
                options.EnableDetailedErrors = true;
   
            });

            services.AddDaprClient();
            services.AddScoped<IEventBus, DaprEventBus>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/error");
            }

            app.UseHealthChecks("/health");
            app.UseOpenApi();

            app.UseSwaggerUi3(settings => {
                settings.Path = "/api";
            });

            app.UseRouting();
            app.UseCloudEvents();

            app.UseCors(c => c
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .WithExposedHeaders(HeaderNames.ContentDisposition));

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles(new StaticFileOptions {
                OnPrepareResponse = ctx => {
                    GZipStaticContent(ctx);
                }
            });

            app.UseResponseCompression();
            app.UseEndpoints(endpoints => {
                endpoints.MapSubscribeHandler();
                endpoints.MapControllers();
                endpoints.MapGrpcService<DaprConsumerServiceGrpc>();
            });
        }

        private static void GZipStaticContent(StaticFileResponseContext ctx)
        {
            var headers = ctx.Context.Response.Headers;
            var contentType = headers["Content-Type"];

            if (contentType != "application/x-gzip" && !ctx.File.Name.EndsWith(".gz")) {
                return;
            }

            var fileNameToTry = ctx.File.Name.Substring(0, ctx.File.Name.Length - 3);

            if (new FileExtensionContentTypeProvider().TryGetContentType(fileNameToTry, out var mimeType)) {
                headers.Add("Content-Encoding", "gzip");
                headers["Content-Type"] = mimeType;
            }
        }

        private static void CacheStaticContent(StaticFileResponseContext ctx)
        {
            const int durationInSeconds = 60 * 60 * 24 * 365;
            ctx.Context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.CacheControl] =
                "public,max-age=" + durationInSeconds;
        }
        private static void AddPermissionPolicies(AuthorizationOptions options)
        {
            foreach (var permission in (PermissionEnum[])Enum.GetValues(typeof(PermissionEnum))) {
                options.AddPolicy(permission + "Policy", p => {
                    p.RequireAuthenticatedUser().AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                    p.AddRequirements(new PermissionRequirement(organizationPermissions: new[] { permission }));
                });
            }
        }
    }
}
