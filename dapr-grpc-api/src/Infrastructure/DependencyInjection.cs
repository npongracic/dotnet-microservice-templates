using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SC.API.CleanArchitecture.Application.Common;
using SC.API.CleanArchitecture.Application.Common.Interfaces;
using SC.API.CleanArchitecture.Domain.Entities;
using SC.API.CleanArchitecture.Infrastructure.Email;
using SC.API.CleanArchitecture.Infrastructure.Identity;
using SC.API.CleanArchitecture.Infrastructure.Persistence;
using SC.API.CleanArchitecture.Infrastructure.Services;
using System;
using System.Reflection;

namespace SC.API.CleanArchitecture.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, string contentRootPath, bool isContextTransient = false)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddDbContext<ApplicationDbContext>(options =>
                options
                    .UseNpgsql(
                        configuration.GetConnectionString("DefaultConnection"),
                        b =>
                        {
                            b.EnableRetryOnFailure()
                             .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                        }).EnableSensitiveDataLogging(), isContextTransient ? ServiceLifetime.Transient : ServiceLifetime.Scoped
            );

            services.AddMemoryCache();
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<IDateTimeService, DateTimeService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<INotificationService, NotificationService>();


            var tokenSettings = configuration.GetSection(nameof(TokenSettings)).Get<TokenSettings>();
            var appSettings = configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();

            services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(options => {
                options.TokenLifespan = TimeSpan.FromDays(7);
            });

            services.AddTransient<IEmailService, EmailService>();
            return services;
        }

    }
}