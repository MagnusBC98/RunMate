using RunMate.Domain.Entities;

namespace RunMate.Application.Users;

/// <summary>
/// Handles database operations relating to running stats.
/// </summary>
public interface IStatsRepository
{
    /// <summary>
    /// Adds a new running stats record.
    /// </summary>
    /// <param name="stats">The stats to add.</param>
    Task AddStatsAsync(RunningStats stats);

    /// <summary>
    /// Gets running stats for a given user.
    /// </summary>
    /// <param name="userId">The ID of the user to retrieve stats for.</param>
    /// <returns>The running stats for the user if they exist; otherwise, null.</returns>
    Task<RunningStats?> GetStatsByUserAsync(Guid userId);

    /// <summary>
    /// Updates running stats with updated field values.
    /// </summary>
    /// <param name="stats">The running stats to update, with updated values.</param>
    Task UpdateUserStatsAsync(RunningStats stats);
}