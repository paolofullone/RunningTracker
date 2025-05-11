using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace RunningTracker.Extensions;

public static class ControllerExtension
{
    public static IServiceCollection AddControllersPolicy(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            // Apply [Authorize] globally
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            options.Filters.Add(new AuthorizeFilter(policy));
        });
        return services;
    }
}
