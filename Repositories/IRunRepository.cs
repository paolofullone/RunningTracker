using RunningTracker.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RunningTracker.Repositories
{
    public interface IRunRepository
    {
        Task<Run> AddRunAsync(Run run);
        Task<IEnumerable<Run>> GetAllRunsAsync();
        Task<Run> GetRunByIdAsync(int id);
        Task<Run> UpdateRunAsync(Run run);
        Task DeleteRunAsync(int id);
    }
}
