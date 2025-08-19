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
}