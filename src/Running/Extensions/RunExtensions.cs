using RunningTracker.Dto;
using RunningTracker.Models;

namespace RunningTracker.Extensions;

public static class RunExtensions
{
    public static RunActivity AddRunAdapt(this RunDto runDto)
    {
        return new RunActivity
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
    }

    public static void UpdateRunFromDto(this RunActivity existingRun, RunDto runDto)
    {
        existingRun.Distance = runDto.Distance;
        existingRun.Duration = runDto.Duration;
        existingRun.Date = runDto.Date.Date;
        existingRun.StartTime = runDto.Date.TimeOfDay;
        existingRun.EndTime = runDto.Date.TimeOfDay.Add(runDto.Duration);
        existingRun.Pace = CalculatePace(runDto.Distance, runDto.Duration);
        existingRun.UpdatedAt = DateTime.UtcNow;
    }

    private static double CalculatePace(double distance, TimeSpan duration)
    {
        return duration.TotalMinutes / distance;
    }
}