using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RunMate.Application.Runs;
using RunMate.Endpoints.Dtos;
using System.Security.Claims;

namespace RunMate.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/runs")]
public class RunsController(IRunsService runsService) : ControllerBase
{
    private readonly IRunsService _runsService = runsService;

    [HttpPost]
    public async Task<IActionResult> CreateRun([FromBody] CreateRunDto request)
    {
        var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var newRun = await _runsService.CreateRunAsync(currentUserId, request.RunDate,
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
        var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var runs = await _runsService.SearchRunsAsync(currentUserId, minDistanceKm, maxDistanceKm, minPace, maxPace);
        return Ok(runs);
    }

    [HttpGet("{runId:guid}")]
    public async Task<IActionResult> GetRunById([FromRoute] Guid runId)
    {
        var run = await _runsService.GetRunByIdAsync(runId);
        return Ok(run);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMyRuns()
    {
        var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var myRuns = await _runsService.GetRunsByUserIdAsync(currentUserId);
        return Ok(myRuns);
    }

    [HttpPut("{runId:guid}")]
    public async Task<IActionResult> UpdateRun(
        [FromRoute] Guid runId,
        [FromBody] UpdateRunDto request)
    {
        var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _runsService.UpdateRunAsync(currentUserId, runId, request.RunDate, request.DistanceInKm, request.AvgPaceInMinutesPerKm);
        return NoContent();
    }

    [HttpDelete("{runId:guid}")]
    public async Task<IActionResult> DeleteRun([FromRoute] Guid runId)
    {
        var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _runsService.DeleteRunAsync(currentUserId, runId);
        return NoContent();
    }
}