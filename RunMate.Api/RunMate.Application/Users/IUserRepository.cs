using RunMate.Domain.Entities;

namespace RunMate.Application.Users;

/// <summary>
/// Handles database operations related to users.
/// </summary>
public interface IUserRepository
{

    /// <summary>
    /// Creates a new user with the given details and password.
    /// </summary>
    /// <param name="user">The domain model representing the user to create.</param>
    /// <param name="password">The password for the user being added.</param>
    /// <returns>The user that has been added, in the form of the User Domain model.</returns>
    /// <exception cref="ValidationException">Thrown if user creation fails due to invalid data (e.g., duplicate email).</exception>
    Task<User> AddUserAsync(User user, string password);

    /// <summary>
    /// Gets a user corresponding to a given ID.
    /// </summary>
    /// <param name="userId">The ID of the user to search for.</param>
    /// <returns>A User model if one exists for the given ID; otherwise, null.</returns>
    Task<User?> GetUserByIdAsync(Guid userId);

    /// <summary>
    /// Updates an existing user's fields.
    /// </summary>
    /// <param name="user">The user to update.</param>
    Task UpdateUserAsync(User user);

    /// <summary>
    /// Gets a user for a given set of credentials.
    /// </summary>
    /// <param name="email">The email address of the user to find.</param>
    /// <param name="password">The password of the user to find.</param>
    /// <returns>A User model if one exists for the given credentials; otherwise, null.</returns>
    Task<User?> GetUserByCredentialsAsync(string email, string password);
}