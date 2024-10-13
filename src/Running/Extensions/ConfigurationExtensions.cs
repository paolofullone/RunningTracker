using RunningTracker.Infra;

namespace RunningTracker.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseConfig>(configuration.GetSection("ConnectionStrings"));
            return services;
        }
    }
}
