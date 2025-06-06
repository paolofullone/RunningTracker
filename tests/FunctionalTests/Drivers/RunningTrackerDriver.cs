﻿using FluentAssertions;
using FunctionalTests.Apis;
using RunningTracker.Dto;
using RunningTracker.Models;

namespace FunctionalTests.Drivers;

public class RunningTrackerDriver
{
    public readonly IRunningTrackerApi _api;
    private readonly List<RunActivity> _runs;
    private readonly List<int> _createdRunIds;

    public RunningTrackerDriver(IRunningTrackerApi api)
    {
        _api = api;
        _runs = new List<RunActivity>();
        _createdRunIds = new List<int>();
    }

    public async Task AddRun(RunDto runDto)
    {
        RunActivity? response = await _api.AddRun(runDto);
        _createdRunIds.Add(response.Id);
        _runs.Add(response);
    }

    public async Task ValidateSucecess(CancellationToken cancellationToken)
    {
        _runs.Should().NotBeEmpty();
        await CleanUp();
    }

    public async Task CleanUp()
    {
        foreach (var runId in _createdRunIds)
        {
            await _api.DeleteRun(runId);
        }
        _createdRunIds.Clear();
    }
}