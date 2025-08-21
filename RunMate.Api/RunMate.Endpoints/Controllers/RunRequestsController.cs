using Microsoft.AspNetCore.Mvc;
using RunMate.Application.Interfaces;
using RunMate.Endpoints.Dtos;

namespace RunMate.Api.Controllers;

[ApiController]
[Route("api")]
public class RunRequestsController : ControllerBase
{
    private IRunRequestsService _runRequestsService;

    public RunRequestsController(IRunRequestsService runRequestsService)
    {
        _runRequestsService = runRequestsService;
    }

    [HttpPost("runs/{runId:guid}/request")]
    public async Task<IActionResult> CreateRunRequest([FromRoute] Guid runId, [FromBody] CreateRunRequestDto requestBody)
    {
        var runRequest = await _runRequestsService.AddRunRequestAsync(runId, requestBody.RequesterUserId);
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
        var runRequests = await _runRequestsService.GetRunRequestsByRunIdAsync(runId);
        return Ok(runRequests);
    }

    [HttpPatch("run-requests/{requestId:guid}")]
    public async Task<IActionResult> UpdateRequestStatus([FromRoute] Guid requestId, [FromBody] UpdateRunRequestStatusDto requestDto)
    {
        var runRequest = await _runRequestsService.UpdateRequestStatusAsync(requestId, requestDto.Status);
        return Ok(runRequest);
    }
}