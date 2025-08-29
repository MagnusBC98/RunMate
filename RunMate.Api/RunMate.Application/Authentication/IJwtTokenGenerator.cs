using RunMate.Domain.Entities;

namespace RunMate.Application.Authentication;

/// <summary>
/// Handles JWT Token generation.
/// </summary>
public interface IJwtTokenGenerator
{
    /// <summary>
    /// Generates a JSON Web Token for the current user.
    /// </summary>
    /// <param name="user">The user the token is being generated for.</param>
    /// <returns>A string containing the encrypted token</returns.>
    string GenerateToken(User user);
}