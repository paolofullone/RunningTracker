using RunningTracker.Infra;
using RunningTracker.Repositories;
using RunningTracker.Services;

namespace RunningTracker.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IRunRepository, RunRepository>();
            services.AddScoped<IRunService, RunService>();
            services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
            return services;
        }
    }
}
