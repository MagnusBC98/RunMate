using RunMate.Domain.Entities;

namespace RunMate.Application.Users;

/// <summary>
/// Handles business logic relating to users.
/// </summary>
public interface IUsersService
{
    /// <summary>
    /// Gets a specific user for a given ID.
    /// </summary>
    /// <param name="userId">The ID of the user to retrieve.</param>
    /// <returns>A user corresponding to the given ID.</returns>
    Task<User> GetUserByIdAsync(Guid userId);

    /// <summary>
    /// Updates a user with new values.
    /// </summary>
    /// <param name="userId">The ID of the user to update.</param>
    /// <param name="FirstName">The updated First Name value.</param>
    /// <param name="LastName">The updated Last Name value.</param>
    Task UpdateUserAsync(Guid userId, string FirstName, string LastName);
}