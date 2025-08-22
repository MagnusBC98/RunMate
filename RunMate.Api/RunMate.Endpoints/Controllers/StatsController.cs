using Microsoft.AspNetCore.Mvc;
using RunMate.Application.Users;
using RunMate.Endpoints.Dtos;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace RunMate.Api.Controllers;

[Authorize]
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

    [HttpPut("me/stats")]
    public async Task<IActionResult> UpdateStats([FromBody] UpdateStatsDto request)
    {
        var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _statsService.UpdateUserStatsAsync(currentUserId, request.FiveKmPb, request.TenKmPb, request.HalfMarathonPb, request.MarathonPb);
        return NoContent();
    }
}