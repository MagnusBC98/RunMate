using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RunMate.Application.Users;
using RunMate.Endpoints.Dtos;
using System.Security.Claims;

namespace RunMate.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/users")]
public class UsersController(IUsersService usersService) : ControllerBase
{
    private readonly IUsersService _usersService = usersService;

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUserById([FromRoute] Guid userId)
    {
        var user = await _usersService.GetUserByIdAsync(userId);
        return Ok(user);
    }

    [HttpPut("me")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto request)
    {
        var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _usersService.UpdateUserAsync(currentUserId, request.FirstName, request.LastName);
        return NoContent();
    }
}