using Microsoft.AspNetCore.Mvc;
using Refit;
using RunningTracker.Dto;
using RunningTracker.Models;

namespace FunctionalTests.Apis
{
    public interface IRunningTrackerApi
    {
        [Get("/api/v1/run")]
        Task<IApiResponse<IEnumerable<Run>>> GetAllRuns();

        [HttpGet("{id}")]
        Task<Run> GetRunById(int id);

        [Post("/api/v1/run")]
        Task<Run> AddRun(RunDto runDto);

        [Delete("/api/v1/run/{id}")]
        Task<IApiResponse> DeleteRun(int id);
    }
}
