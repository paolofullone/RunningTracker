using RunningTracker.Infrastructure.Database;
using RunningTracker.Infrastructure.Repositories.Identity;
using RunningTracker.Infrastructure.Repositories.Run;
using RunningTracker.Services;
using RunningTracker.Services.Identity;

namespace RunningTracker.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IRunRepository, RunRepository>();
        services.AddScoped<IRunService, RunService>();
        services.AddSingleton<IIdentityService, IdentityService>();
        services.AddSingleton<IIdentityRepository, IdentityRepository>();
        services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
        return services;
    }
}
