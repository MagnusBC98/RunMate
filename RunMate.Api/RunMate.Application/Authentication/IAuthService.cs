using RunMate.Domain.Entities;

namespace RunMate.Application.Authentication;

/// <summary>
/// Handles business logic for user authentication.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Registers a new user in the system.
    /// </summary>
    /// <param name="email">The user's email address.</param>
    /// <param name="password">The user's password.</param>
    /// <param name="firstName">The user's first name.</param>
    /// <param name="lastName">The user's last name.</param>
    /// <returns>The newly created User domain entity.</returns>
    Task<User> RegisterUserAsync(string email, string password, string firstName, string lastName);

    /// <summary>
    /// Authenticates a user with their credentials.
    /// </summary>
    /// <param name="email">The user's email address.</param>
    /// <param name="password">The user's password.</param>
    /// <returns>A JWT token if the credentials are valid; otherwise, null.</returns>
    Task<string?> LoginAsync(string email, string password);
}