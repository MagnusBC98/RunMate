using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RunMate.Application.RunRequests;
using RunMate.Application.Runs.Dtos;
using RunMate.Domain.Entities;
using RunMate.Endpoints.Dtos;
using System.Security.Claims;

namespace RunMate.Api.Controllers;

/// <summary>
/// Manages Run Request related endpoints.
/// </summary>
[Authorize]
[ApiController]
[Route("api")]
public class RunRequestsController(IRunRequestsService runRequestsService) : ControllerBase
{
    private readonly IRunRequestsService _runRequestsService = runRequestsService;

    /// <summary>
    /// Creates a new run request.
    /// </summary>
    /// <param name="runId">The ID of the run the request is being made to.</param>
    /// <response code="201">If a run request was successfully created.</response>
    /// <response code="404">If a run with the specified ID is not found.</response>
    [HttpPost("runs/{runId:guid}/request")]
    [ProducesResponseType(typeof(RunRequest), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateRunRequest([FromRoute] Guid runId)
    {
        var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var runRequest = await _runRequestsService.AddRunRequestAsync(runId, currentUserId);
        return CreatedAtAction(nameof(CreateRunRequest), new { requestId = runRequest.Id }, runRequest);
    }

    /// <summary>
    /// Gets a run request by its ID.
    /// </summary>
    /// <param name="requestId">The ID of the run request to find.</param>
    /// <response code="200">Returns the run request corresponding to the ID.</response>
    /// <response code="404">If a run request with the specified ID is not found.</response>
    [HttpGet("run-requests/{requestId:guid}")]
    [ProducesResponseType(typeof(RunRequest), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRunRequestById([FromRoute] Guid requestId)
    {
        var runRequest = await _runRequestsService.GetRunRequestByIdAsync(requestId);
        return Ok(runRequest);
    }

    /// <summary>
    /// Gets the run requests sent to a specific run.
    /// </summary>
    /// <param name="runId">The ID of the run to find requests for.</param>
    /// <response code="200">Returns the run requests sent to the run with the given ID.</response>
    /// <response code="401">If the user making the request does not own the run with the given ID.</response>
    /// <response code="404">If a run with the specified ID is not found.</response>
    [HttpGet("runs/{runId:guid}/requests")]
    [ProducesResponseType(typeof(IEnumerable<GetRunRequestsResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRunRequestsByRunId([FromRoute] Guid runId)
    {
        var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var runRequests = await _runRequestsService.GetRunRequestsByRunIdAsync(currentUserId, runId);
        return Ok(runRequests);
    }

    /// <summary>
    /// Updates the status of a run request.
    /// </summary>
    /// <param name="requestId">The ID of the request to update.</param>
    /// <param name="requestDto">The DTO containing the new status.</param>
    /// <response code="200">Returns the newly updated run request.</response>
    /// <response code="400">If an invalid status is provided.</response>
    /// <response code="401">If the user updating the status is not the owner of the run the request was made to.</response>
    /// <response code="404">If a run request with the specified ID is not found.</response>
    [HttpPatch("run-requests/{requestId:guid}")]
    [ProducesResponseType(typeof(RunRequest), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRequestStatus([FromRoute] Guid requestId, [FromBody] UpdateRunRequestStatusDto requestDto)
    {
        var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var runRequest = await _runRequestsService.UpdateRequestStatusAsync(currentUserId, requestId, requestDto.Status);
        return Ok(runRequest);
    }
}