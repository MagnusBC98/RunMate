using RunMate.Domain.Entities;
using RunMate.Application.Runs.Dtos;

namespace RunMate.Application.Runs;

/// <summary>
/// Handles database operations relating to runs.
/// </summary>
public interface IRunsRepository
{
    /// <summary>
    /// Adds a new run.
    /// </summary>
    /// <param name="run">The run to add.</param>
    /// <returns>The newly created run.</returns>
    Task<Run> AddRunAsync(Run run);

    /// <summary>
    /// Searches for runs matching various filters.
    /// </summary>
    /// <param name="currentUserId">The ID of the current logged in user.</param>
    /// <param name="minDistanceKm">The minimum distance in KM to search for.</param>
    /// <param name="maxDistanceKm">The maximum distance in KM to search for.</param>
    /// <param name="minPace">The minimum pace in minutes per KM to search for (syntax 00:00:00).</param>
    /// <param name="maxPace">The maximum pace in minutes per KM to search for (syntax 00:00:00).</param>
    /// <returns>A collection of <see cref="SearchRunsResult"/> objects.</returns>
    Task<IEnumerable<SearchRunsResult>> SearchRunsAsync(Guid currentUserId, double? minDistanceKm,
        double? maxDistanceKm, TimeSpan? minPace, TimeSpan? maxPace);

    /// <summary>
    /// Gets a specific run for a given ID.
    /// </summary>
    /// <param name="runId">The ID of the run to find.</param>
    /// <returns>A run if one exists for the given ID; otherwise, null.</returns>
    Task<Run?> GetRunByIdAsync(Guid runId);

    /// <summary>
    /// Gets all runs for a given user.
    /// </summary>
    /// <param name="userId">The ID of the user to get runs for.</param>
    /// <returns>A collection of runs for the given user.</returns>
    Task<IEnumerable<Run>> GetRunsByUserIdAsync(Guid userId);

    /// <summary>
    /// Updates the fields for an existing run.
    /// </summary>
    /// <param name="run">The run to update, with updated field values.</param>
    Task UpdateRunAsync(Run run);

    /// <summary>
    /// Deletes a run.
    /// </summary>
    /// <param name="run">The run to delete.</param>
    Task DeleteRunAsync(Run run);
}