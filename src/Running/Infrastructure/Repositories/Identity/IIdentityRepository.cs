namespace RunningTracker.Infrastructure.Repositories.Identity;

public interface IIdentityRepository
{
    Task<Models.User> GetByEmailAsync(string email);
    Task CreateUserAsync(Models.User user);
}
