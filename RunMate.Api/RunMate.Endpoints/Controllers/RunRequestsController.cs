using Microsoft.AspNetCore.Mvc;
using RunMate.Application.Interfaces;

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
}