using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RunMate.Application.RunMates;
using RunMate.Application.RunMates.Dtos;
using System.Security.Claims;

namespace RunMate.Api.Controllers;

/// <summary>
/// Manages Run Mate related endpoints.
/// </summary>
[Authorize]
[ApiController]
[Route("api/runmates")]
public class RunMatesController(IRunMatesService runMatesService) : ControllerBase
{
    private readonly IRunMatesService _runMatesService = runMatesService;

    /// <summary>
    /// Gets a list of Run Mates for a user.
    /// </summary>
    /// <response code="200">Returns a list of the user's Run Mates.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RunMateResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRunMates()
    {
        var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var runMates = await _runMatesService.GetRunMatesAsync(currentUserId);
        return Ok(runMates);
    }
}