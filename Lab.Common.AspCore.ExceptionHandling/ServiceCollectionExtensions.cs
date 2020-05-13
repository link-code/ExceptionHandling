namespace Lab.Common.AspCore.ExceptionHandling
{
    using System.Reflection;
    using Lab.Common.AspCore.ExceptionHandling.Handlers;
    using Lab.Common.AspCore.ExceptionHandling.Middlewares;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddExceptionHandling(this IServiceCollection services, params Assembly[] assemblies)
        {
            services
                .Scan(scan => scan
                .FromAssemblies(assemblies)
                .AddClasses(cl => cl.AssignableTo<ExceptionHandler>())
                .As<ExceptionHandler>());

            services
                .AddScoped<ExceptionHandler, AggregateExceptionHandler>();

            services.AddScoped<ExceptionHandlingMiddleware>();

            return services;
        }
    }
}
