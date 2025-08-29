using Microsoft.AspNetCore.Mvc;
using RunMate.Application.Users;
using RunMate.Endpoints.Dtos;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using RunMate.Domain.Entities;

namespace RunMate.Api.Controllers;

/// <summary>
/// Manages endpoints relating to running stats.
/// </summary>
[Authorize]
[ApiController]
[Route("api/users")]
public class StatsController(IStatsService statsService) : ControllerBase
{
    private readonly IStatsService _statsService = statsService;

    /// <summary>
    /// Gets stats for a particular user.
    /// </summary>
    /// <param name="userId">The ID of the user to get stats for.</param>
    /// <response code="200">Returns the user's stats.</response>
    /// <response code="404">If a stats for the given user ID were not found.</response>
    [HttpGet("{userId:guid}/stats")]
    [ProducesResponseType(typeof(RunningStats), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetStats([FromRoute] Guid userId)
    {
        var stats = await _statsService.GetStatsByUserAsync(userId);
        return Ok(stats);
    }

    /// <summary>
    /// Updates a user's stats.
    /// </summary>
    /// <param name="request">The DTO containing the stats to update.</param>
    /// <response code="204">If the stats were successfully updated.</response>
    /// <response code="404">If a stats for the given user ID were not found.</response>    
    [HttpPut("me/stats")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStats([FromBody] UpdateStatsDto request)
    {
        var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _statsService.UpdateUserStatsAsync(currentUserId, request.FiveKmPb, request.TenKmPb, request.HalfMarathonPb, request.MarathonPb);
        return NoContent();
    }
}