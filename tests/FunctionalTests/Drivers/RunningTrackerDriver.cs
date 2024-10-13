using FluentAssertions;
using FunctionalTests.Apis;
using Microsoft.AspNetCore.Mvc;
using Refit;
using RunningTracker.Dto;
using RunningTracker.Models;

namespace FunctionalTests.Drivers
{
    public class RunningTrackerDriver
    {
        public readonly IRunningTrackerApi _api;
        private readonly List<Run> _runs;

        public RunningTrackerDriver(IRunningTrackerApi api)
        {
            _api = api;
            _runs = new List<Run>();
        }

        public async Task AddRun(RunDto runDto)
        {
            Run? response = await _api.AddRun(runDto);
            _runs.Add(response);
        }

        public async Task ValidateSucecess(CancellationToken cancellationToken)
        {
            _runs.Should().NotBeEmpty();
        }
    }
}
