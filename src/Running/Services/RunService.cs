using RunningTracker.Dto;
using RunningTracker.Models;
using RunningTracker.Repositories;

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
            var run = new Run
            {
                Distance = runDto.Distance,
                Duration = runDto.Duration,
                Date = runDto.Date.Date,
                StartTime = runDto.Date.TimeOfDay,
                EndTime = runDto.Date.TimeOfDay.Add(runDto.Duration),
                Pace = CalculatePace(runDto.Distance, runDto.Duration),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

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

            existingRun.Distance = runDto.Distance;
            existingRun.Duration = runDto.Duration;
            existingRun.Date = runDto.Date.Date;
            existingRun.StartTime = runDto.Date.TimeOfDay;
            existingRun.EndTime = runDto.Date.TimeOfDay.Add(runDto.Duration);
            existingRun.Pace = CalculatePace(runDto.Distance, runDto.Duration);
            existingRun.UpdatedAt = DateTime.UtcNow;

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

        private double CalculatePace(double distance, TimeSpan duration)
        {
            return duration.TotalMinutes / distance;
        }


    }
}
