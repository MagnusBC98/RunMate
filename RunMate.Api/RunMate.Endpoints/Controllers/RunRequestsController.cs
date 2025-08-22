using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RunMate.Application.RunRequests;
using RunMate.Endpoints.Dtos;
using System.Security.Claims;

namespace RunMate.Api.Controllers;

[Authorize]
[ApiController]
[Route("api")]
public class RunRequestsController(IRunRequestsService runRequestsService) : ControllerBase
{
    private readonly IRunRequestsService _runRequestsService = runRequestsService;

    [HttpPost("runs/{runId:guid}/request")]
    public async Task<IActionResult> CreateRunRequest([FromRoute] Guid runId)
    {
        var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var runRequest = await _runRequestsService.AddRunRequestAsync(runId, currentUserId);
        return CreatedAtAction(nameof(CreateRunRequest), new { requestId = runRequest.Id });
    }

    [HttpGet("run-requests/{requestId:guid}")]
    public async Task<IActionResult> GetRunRequestById([FromRoute] Guid requestId)
    {
        var runRequest = await _runRequestsService.GetRunRequestByIdAsync(requestId);
        return Ok(runRequest);
    }

    [HttpGet("runs/{runId:guid}/requests")]
    public async Task<IActionResult> GetRunRequestsByRunId([FromRoute] Guid runId)
    {
        var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var runRequests = await _runRequestsService.GetRunRequestsByRunIdAsync(currentUserId, runId);
        return Ok(runRequests);
    }

    [HttpPatch("run-requests/{requestId:guid}")]
    public async Task<IActionResult> UpdateRequestStatus([FromRoute] Guid requestId, [FromBody] UpdateRunRequestStatusDto requestDto)
    {
        var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var runRequest = await _runRequestsService.UpdateRequestStatusAsync(currentUserId, requestId, requestDto.Status);
        return Ok(runRequest);
    }
}