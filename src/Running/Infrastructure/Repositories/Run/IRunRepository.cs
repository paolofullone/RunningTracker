using RunningTracker.Models;

namespace RunningTracker.Infrastructure.Repositories.Run;

public interface IRunRepository
{
    Task<RunActivity> AddRunAsync(RunActivity run);
    Task<IEnumerable<RunActivity>> GetAllRunsAsync();
    Task<IEnumerable<RunActivity>> GetRunsByDateAsync(DateTime date);
    Task<RunActivity> GetRunByIdAsync(int id);
    Task<RunActivity> UpdateRunAsync(RunActivity run);
    Task DeleteRunAsync(int id);
    Task DeleteAllRunsAsync();
}
