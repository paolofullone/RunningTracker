using Microsoft.AspNetCore.Mvc;
using Refit;
using RunningTracker.Dto;
using RunningTracker.Models;

namespace FunctionalTests.Apis;

public interface IRunningTrackerApi
{
    [Get("/api/v1/run")]
    Task<IApiResponse<IEnumerable<RunActivity>>> GetAllRuns();

    [HttpGet("{id}")]
    Task<RunActivity> GetRunById(int id);

    [Post("/api/v1/run")]
    Task<RunActivity> AddRun(RunDto runDto);

    [Delete("/api/v1/run/{id}")]
    Task<IApiResponse> DeleteRun(int id);
}
