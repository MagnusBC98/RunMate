namespace RunMate.Endpoints.Dtos;

/// <summary>
/// Represents the data required for a user to login.
/// </summary>
/// <param name="Email">The user's email address.</param>
/// <param name="Password">The user's password.</param>
public record LoginRequestDto(string Email, string Password);