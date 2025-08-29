using RunMate.Application.RunMates.Dtos;

namespace RunMate.Application.RunMates;

/// <summary>
/// Handles business logic related to RunMates 
/// (users who have been matched through an accepted run request)
/// </summary>
public interface IRunMatesService
{
    /// <summary>
    /// Gets all RunMates for the given user.
    /// </summary>
    /// <param name="userId">The user ID to find RunMates for.</param>
    /// <returns>A collection of <see cref="RunMateResult"/> objects.</returns>
    Task<IEnumerable<RunMateResult>> GetRunMatesAsync(Guid userId);
}