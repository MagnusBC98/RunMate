using Microsoft.AspNetCore.Mvc;
using RunMate.Application.Interfaces;
using RunMate.Endpoints.Dtos;

namespace RunMate.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUserById([FromRoute] Guid userId)
    {
        var user = await _usersService.GetUserByIdAsync(userId);

        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPut("{userId:guid}")]
    public async Task<IActionResult> UpdateUser([FromRoute] Guid userId, [FromBody] UpdateUserDto updateDto)
    {
        await _usersService.UpdateUserAsync(userId, updateDto.FirstName, updateDto.LastName);

        return NoContent();
    }
}