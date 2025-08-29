namespace RunMate.Endpoints.Dtos;

/// <summary>
/// Represents the user data that can be updated.
/// </summary>
/// <param name="FirstName">The user's first name.</param>
/// <param name="LastName">The user's last name.</param>
public record UpdateUserDto(string FirstName, string LastName);