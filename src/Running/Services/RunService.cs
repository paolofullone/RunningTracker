using RunningTracker.Dto;
using RunningTracker.Extensions;
using RunningTracker.Infrastructure.Repositories.Run;
using RunningTracker.Models;

namespace RunningTracker.Services;

public class RunService : IRunService
{
    private readonly IRunRepository _runRepository;

    public RunService(IRunRepository runRepository)
    {
        _runRepository = runRepository;
    }

    public async Task<RunActivity> AddRunAsync(RunDto runDto)
    {
        var run = runDto.AddRunAdapt();

        return await _runRepository.AddRunAsync(run);
    }

    public async Task<IEnumerable<RunActivity>> GetAllRunsAsync()
    {
        return await _runRepository.GetAllRunsAsync();
    }

    public async Task<IEnumerable<RunActivity>> GetRunsByDateAsync(DateTime date)
    {
        return await _runRepository.GetRunsByDateAsync(date);
    }

    public async Task<RunActivity> GetRunByIdAsync(int id)
    {
        return await _runRepository.GetRunByIdAsync(id);
    }

    public async Task<RunActivity> UpdateRunAsync(int id, RunDto runDto)
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
