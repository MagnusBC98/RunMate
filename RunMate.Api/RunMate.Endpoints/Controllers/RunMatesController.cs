using Microsoft.AspNetCore.Mvc;
using RunMate.Application.RunMates;
using System.Security.Claims;

namespace RunMate.Api.Controllers;

[ApiController]
[Route("api/runmates")]
public class RunMatesController(IRunMatesService runMatesService) : ControllerBase
{
    private readonly IRunMatesService _runMatesService = runMatesService;

    [HttpGet]
    public async Task<IActionResult> GetRunMates()
    {
        var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var runMates = await _runMatesService.GetRunMatesAsync(currentUserId);
        return Ok(runMates);
    }
}