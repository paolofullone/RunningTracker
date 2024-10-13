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
            if (runs is null) return new List<Run>();
            return Ok(runs);
        }

        [HttpGet("date/{date}")]
        public async Task<ActionResult<IEnumerable<Run>>> GetRunsByDate(DateTime date)
        {
            var runs = await _runService.GetRunsByDateAsync(date);
            if (runs is null) return new List<Run>();
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
        public async Task<ActionResult<Run>> UpdateRunAsync(int id, RunDto runDto)
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
        public async Task<IActionResult> DeleteRunAsync(int id)
        {
            await _runService.DeleteRunAsync(id);
            return NoContent();
        }

        [HttpDelete("/all")]
        public async Task<IActionResult> DeleteAllRuns()
        {
            await _runService.DeleteAllRunsAsync();
            return NoContent();
        }
    }
}
