using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunMate.Infrastructure.Identity;
using RunMate.Application.Interfaces;
using RunMate.Endpoints.Dtos;

namespace RunMate.Api.Controllers;

[ApiController]
[Route("api/users")]
public class StatsController : ControllerBase
{
    private IStatsService _statsService;

    public StatsController(IStatsService statsService)
    {
        _statsService = statsService;
    }

    [HttpGet("{userId:guid}/stats")]
    public async Task<IActionResult> GetStats([FromRoute] Guid userId)
    {
        var stats = await _statsService.GetStatsByUserAsync(userId);
        return Ok(stats);
    }

    [HttpPut("{userId:guid}/stats")]
    public async Task<IActionResult> UpdateStats([FromRoute] Guid userId, [FromBody] UpdateStatsDto request)
    {
        await _statsService.UpdateUserStatsAsync(userId, request.FiveKmPb, request.TenKmPb, request.HalfMarathonPb, request.MarathonPb);
        return NoContent();
    }
}