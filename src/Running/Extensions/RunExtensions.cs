using System;
using RunningTracker.Models;
using RunningTracker.Dto;

namespace RunningTracker.Utils
{
    public static class RunExtensions
    {
        public static Run AddRunAdapt(this RunDto runDto)
        {
            return new Run
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

        public static void UpdateRunFromDto(this Run existingRun, RunDto runDto)
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
}