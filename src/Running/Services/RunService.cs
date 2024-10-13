using RunningTracker.Dto;
using RunningTracker.Models;
using RunningTracker.Repositories;
using RunningTracker.Utils;

namespace RunningTracker.Services
{
    public class RunService : IRunService
    {
        private readonly IRunRepository _runRepository;

        public RunService(IRunRepository runRepository)
        {
            _runRepository = runRepository;
        }

        public async Task<Run> AddRunAsync(RunDto runDto)
        {
            var run = runDto.AddRunAdapt();

            return await _runRepository.AddRunAsync(run);
        }

        public async Task<IEnumerable<Run>> GetAllRunsAsync()
        {
            return await _runRepository.GetAllRunsAsync();
        }

        public async Task<IEnumerable<Run>> GetRunsByDateAsync(DateTime date)
        {
            return await _runRepository.GetRunsByDateAsync(date);
        }

        public async Task<Run> GetRunByIdAsync(int id)
        {
            return await _runRepository.GetRunByIdAsync(id);
        }

        public async Task<Run> UpdateRunAsync(int id, RunDto runDto)
        {
            var existingRun = await _runRepository.GetRunByIdAsync(id);
            if (existingRun == null)
            {
                throw new KeyNotFoundException("Run not found");
            }

            existingRun.UpdateRunFromDto(runDto);

            return await _runRepository.UpdateRunAsync(existingRun);
        }

        public async Task DeleteRunAsync(int id)
        {
            await _runRepository.DeleteRunAsync(id);
        }

        public async Task DeleteAllRunsAsync()
        {
            await _runRepository.DeleteAllRunsAsync();
        }
    }
}
