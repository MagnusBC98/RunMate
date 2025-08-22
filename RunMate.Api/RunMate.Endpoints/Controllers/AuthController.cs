using Microsoft.AspNetCore.Mvc;
using RunMate.Application.Authentication;
using RunMate.Endpoints.Dtos;

namespace RunMate.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto request)
    {

        var newUser = await _authService.RegisterUserAsync(request.FirstName, request.LastName,
        request.Email, request.Password);

        return CreatedAtAction(nameof(Register), new { userId = newUser.Id });
    }

    [HttpPost("login")]
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