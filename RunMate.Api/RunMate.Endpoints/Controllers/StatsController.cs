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

        if (stats is null)
        {
            return NotFound();
        }

        return Ok(stats);
    }

    [HttpPut("{userId:guid}/stats")]
    public async Task<IActionResult> UpdateStats([FromRoute] Guid userId, [FromBody] UpdateStatsDto updateDto)
    {
        TimeSpan.TryParse(updateDto.FiveKmPb, out var fiveKmPb);
        TimeSpan.TryParse(updateDto.TenKmPb, out var tenKmPb);
        TimeSpan.TryParse(updateDto.HalfMarathonPb, out var halfMarathonPb);
        TimeSpan.TryParse(updateDto.MarathonPb, out var marathonPb);

        await _statsService.UpdateUserStatsAsync(userId, fiveKmPb, tenKmPb, halfMarathonPb, marathonPb);

        return NoContent();
    }
}