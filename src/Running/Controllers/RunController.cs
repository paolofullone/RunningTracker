using Microsoft.AspNetCore.Mvc;
using RunningTracker.Dto;
using RunningTracker.Models;
using RunningTracker.Services;

namespace RunningTracker.Controllers
{
    [ApiController]
    [Route("api/v1/run")]
    [ApiVersion("1.0")]
    public class RunController : ControllerBase
    {
        private readonly IRunService _runService;

        public RunController(IRunService runService)
        {
            _runService = runService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Run>>> GetAllRuns()
        {
            var runs = await _runService.GetAllRunsAsync();
            return Ok(runs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Run>> GetRunById(int id)
        {
            var run = await _runService.GetRunByIdAsync(id);
            if (run == null)
            {
                return NotFound();
            }
            return Ok(run);
        }

        [HttpPost]
        public async Task<ActionResult<Run>> AddRun(RunDto runDto)
        {
            var run = await _runService.AddRunAsync(runDto);
            return CreatedAtAction(nameof(GetRunById), new { id = run.Id }, run);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Run>> UpdateRun(int id, RunDto runDto)
        {
            try
            {
                var updatedRun = await _runService.UpdateRunAsync(id, runDto);
                return Ok(updatedRun);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRun(int id)
        {
            await _runService.DeleteRunAsync(id);
            return NoContent();
        }
    }
}
