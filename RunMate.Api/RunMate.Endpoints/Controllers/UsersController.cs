using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RunMate.Application.Users;
using RunMate.Domain.Entities;
using RunMate.Endpoints.Dtos;
using System.Security.Claims;

namespace RunMate.Api.Controllers;

/// <summary>
/// Manages endpoints related to users.
/// </summary>
[Authorize]
[ApiController]
[Route("api/users")]
public class UsersController(IUsersService usersService) : ControllerBase
{
    private readonly IUsersService _usersService = usersService;

    /// <summary>
    /// Gets a user for a given ID.
    /// </summary>
    /// <param name="userId">The ID of the user to find.</param>
    /// <response code="200">Returns the user for the given ID.</response>
    /// <response code="404">If a user with the given ID was not found.</response>
    [HttpGet("{userId:guid}")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserById([FromRoute] Guid userId)
    {
        var user = await _usersService.GetUserByIdAsync(userId);
        return Ok(user);
    }

    /// <summary>
    /// Updates a user's information.
    /// </summary>
    /// <param name="request">The DTO containing the user details to update.</param>
    /// <response code="204">If the user was successfully updated.</response>
    /// <response code="404">If a user with the given ID was not found.</response>
    [HttpPut("me")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto request)
    {
        var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _usersService.UpdateUserAsync(currentUserId, request.FirstName, request.LastName);
        return NoContent();
    }
}