using Microsoft.AspNetCore.Mvc;
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
        var newRun = await _runsService.CreateRunAsync(request.UserId, request.RunDate,
        request.DistanceInKm, request.AvgPaceInMinutesPerKm);

        return CreatedAtAction(nameof(CreateRun), new { runId = newRun.Id });
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
        return Ok(run);
    }

    [HttpPut("{runId:guid}")]
    public async Task<IActionResult> UpdateRun(
        [FromRoute] Guid runId,
        [FromBody] UpdateRunDto request)
    {
        await _runsService.UpdateRunAsync(runId, request.RunDate, request.DistanceInKm, request.AvgPaceInMinutesPerKm);
        return NoContent();
    }

    [HttpDelete("{runId:guid}")]
    public async Task<IActionResult> DeleteRun([FromRoute] Guid runId)
    {
        await _runsService.DeleteRunAsync(runId);
        return Ok();
    }
}