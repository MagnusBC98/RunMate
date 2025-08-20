using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunMate.Infrastructure.Identity;
using RunMate.Application.Interfaces;
using RunMate.Endpoints.Dtos;

namespace RunMate.Api.Controllers;

[ApiController]
[Route("api/runs")]
public class RunsController : ControllerBase
{
    private IRunsService _runsService;

    public RunsController(IRunsService runsService)
    {
        _runsService = runsService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRun([FromBody] CreateRunDto request)
    {
        try
        {
            if (!TimeSpan.TryParse(request.AvgPaceInMinutesPerKm, out var pace))
            {
                return BadRequest("Invalid pace format. Please use hh:mm:ss.");
            }

            var newRun = await _runsService.CreateRunAsync(request.UserId, request.RunDate,
            request.DistanceInKm, pace);

            return CreatedAtAction(nameof(CreateRun), new { runId = newRun.Id });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> SearchRuns(
        [FromQuery] double? minDistanceKm,
        [FromQuery] double? maxDistanceKm,
        [FromQuery] TimeSpan? minPace,
        [FromQuery] TimeSpan? maxPace)
    {
        var runs = await _runsService.SearchRunsAsync(minDistanceKm, maxDistanceKm, minPace, maxPace);
        return Ok(runs);
    }

    [HttpGet("{runId:guid}")]
    public async Task<IActionResult> GetRunById([FromRoute] Guid runId)
    {
        var run = await _runsService.GetRunByIdAsync(runId);

        if (run is null)
        {
            return NotFound();
        }

        return Ok(run);
    }

    [HttpPut("{runId:guid}")]
    public async Task<IActionResult> UpdateRun(
        [FromRoute] Guid runId,
        [FromBody] UpdateRunDto request)
    {
        TimeSpan.TryParse(request.AvgPaceInMinutesPerKm, out var avgPace);

        await _runsService.UpdateRunAsync(runId, request.RunDate, request.DistanceInKm, avgPace);

        return NoContent();
    }
}