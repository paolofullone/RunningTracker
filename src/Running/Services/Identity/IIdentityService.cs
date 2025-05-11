using RunningTracker.Models;

namespace RunningTracker.Services.Identity;

public interface IIdentityService
{
    Task<string> AuthenticateAsync(Login login);
    Task RegisterAsync(Register register);
}
