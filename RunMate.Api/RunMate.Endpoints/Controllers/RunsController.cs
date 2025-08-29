using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RunMate.Application.Runs;
using RunMate.Application.Runs.Dtos;
using RunMate.Domain.Entities;
using RunMate.Endpoints.Dtos;
using System.Security.Claims;

namespace RunMate.Api.Controllers;

/// <summary>
/// Manages Run related endpoints.
/// </summary>
[Authorize]
[ApiController]
[Route("api/runs")]
public class RunsController(IRunsService runsService) : ControllerBase
{
    private readonly IRunsService _runsService = runsService;

    /// <summary>
    /// Creates a new run.
    /// </summary>
    /// <param name="request">The DTO containing the run data.</param>
    /// <response code="201">If a run was successfully created.</response>
    /// <response code="400">If an invalid request was made (e.g. distance below 0).</response>
    [HttpPost]
    [ProducesResponseType(typeof(Run), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateRun([FromBody] CreateRunDto request)
    {
        var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var newRun = await _runsService.CreateRunAsync(currentUserId, request.RunDate,
        request.DistanceInKm, request.AvgPaceInMinutesPerKm);

        return CreatedAtAction(nameof(CreateRun), new { runId = newRun.Id }, newRun);
    }

    /// <summary>
    /// Searches for runs corresponding to given parameters.
    /// </summary>
    /// <param name="minDistanceKm">The minimum distance of the run.</param>
    /// <param name="maxDistanceKm">The maximum distance of the run.</param>
    /// <param name="minPace">The minimum pace of the run (in minutes per kilometre)</param>
    /// <param name="maxPace">The maximum pace of the run (in minutes per kilometre)</param>
    /// <response code="200">Returns the runs for the given parameters.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SearchRunsResult>), StatusCodes.Status200OK)]
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

    /// <summary>
    /// Gets a run by its ID.
    /// </summary>
    /// <param name="runId">The ID of the run to find.</param>
    /// <response code="200">Returns a run with the given ID.</response>
    /// <response code="404">If a run with the specified ID is not found.</response>
    [HttpGet("{runId:guid}")]
    [ProducesResponseType(typeof(Run), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRunById([FromRoute] Guid runId)
    {
        var run = await _runsService.GetRunByIdAsync(runId);
        return Ok(run);
    }

    /// <summary>
    /// Gets runs belonging to the logged in user.
    /// </summary>
    /// <response code="200">Returns the user's runs.</response>
    [HttpGet("me")]
    [ProducesResponseType(typeof(IEnumerable<Run>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyRuns()
    {
        var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var myRuns = await _runsService.GetRunsByUserIdAsync(currentUserId);
        return Ok(myRuns);
    }

    /// <summary>
    /// Updates a run's values.
    /// </summary>
    /// <param name="runId">The ID of the run to update.</param>
    /// <param name="request">The DTO containing the run values to update.</param>
    /// <response code="204">If the run was successfully updated.</response>
    /// <response code="400">If an invalid request is made (e.g. distance below 0).</response>
    /// <response code="401">If the user updating the run is not the owner of the run.</response>
    /// <response code="404">If a run with the specified ID is not found.</response>
    [HttpPut("{runId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRun(
        [FromRoute] Guid runId,
        [FromBody] UpdateRunDto request)
    {
        var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _runsService.UpdateRunAsync(currentUserId, runId, request.RunDate, request.DistanceInKm, request.AvgPaceInMinutesPerKm);
        return NoContent();
    }

    /// <summary>
    /// Deletes a run.
    /// </summary>
    /// <param name="runId">The ID of the run to delete</param>
    /// <response code="204">If the run was successfully deleted.</response>
    /// <response code="401">If the user deleting the run is not the owner of the run.</response>
    /// <response code="404">If a run with the specified ID is not found.</response>
    [HttpDelete("{runId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRun([FromRoute] Guid runId)
    {
        var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _runsService.DeleteRunAsync(currentUserId, runId);
        return NoContent();
    }
}