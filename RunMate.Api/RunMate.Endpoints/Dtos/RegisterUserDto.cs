namespace RunMate.Endpoints.Dtos;

/// <summary>
/// Represents the data required to register a new user.
/// </summary>
/// <param name="Email">The user's email address.</param>
/// <param name="Password">The user's password.</param>
/// <param name="FirstName">The user's first name.</param>
/// <param name="LastName">The user's last name.</param>
public record RegisterUserDto(string Email, string Password, string FirstName, string LastName);