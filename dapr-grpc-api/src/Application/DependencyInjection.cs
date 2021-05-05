using AutoMapper;
using SC.API.CleanArchitecture.Application.Common;
using SC.API.CleanArchitecture.Application.Common.Behaviours;
using SC.API.CleanArchitecture.Application.Common.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace SC.API.CleanArchitecture.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            var appSettings = configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
            services.AddSingleton(appSettings);

            return services;
        }
    }
}
