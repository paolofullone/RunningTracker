using RunningTracker.Dto;
using RunningTracker.Models;

namespace RunningTracker.Services;

public interface IRunService
{
    Task<RunActivity> AddRunAsync(RunDto runDto);
    Task<IEnumerable<RunActivity>> GetAllRunsAsync();
    Task<IEnumerable<RunActivity>> GetRunsByDateAsync(DateTime date);
    Task<RunActivity> GetRunByIdAsync(int id);
    Task<RunActivity> UpdateRunAsync(int id, RunDto runDto);
    Task DeleteRunAsync(int id);
    Task DeleteAllRunsAsync();
}
