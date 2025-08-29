using Microsoft.AspNetCore.Mvc;
using RunMate.Application.Authentication;
using RunMate.Endpoints.Dtos;

namespace RunMate.Api.Controllers;

/// <summary>
/// Manages authorization-related endpoints.
/// </summary>
/// <param name="authService"></param>
[ApiController]
[Route("api/auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="request">The DTO containing the user's details.</param>
    /// <response code="201">If a user was successfully registered.</response>
    /// <response code="400">If the request is invalid (e.g., duplicate email).</response>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto request)
    {

        var newUser = await _authService.RegisterUserAsync(request.FirstName, request.LastName,
        request.Email, request.Password);

        return CreatedAtAction(nameof(Register), new { userId = newUser.Id });
    }

    /// <summary>
    /// Authenticates a user with their credentials.
    /// </summary>
    /// <param name="request">A DTO containing the user's email and password.</param>
    /// <response code="200">Returns a JWT token upon successful authentication.</response>
    /// <response code="401">If the provided credentials are invalid.</response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        var token = await _authService.LoginAsync(request.Email, request.Password);

        if (token is null)
        {
            return Unauthorized();
        }

        return Ok(new { Token = token });
    }
}