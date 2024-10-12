using RunningTracker.Dto;
using RunningTracker.Models;

namespace RunningTracker.Services
{
    public interface IRunService
    {
        Task<Run> AddRunAsync(RunDto runDto);
        Task<IEnumerable<Run>> GetAllRunsAsync();
        Task<Run> GetRunByIdAsync(int id);
        Task<Run> UpdateRunAsync(int id, RunDto runDto);
        Task DeleteRunAsync(int id);
    }
}
