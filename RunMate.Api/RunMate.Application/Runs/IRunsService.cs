using RunMate.Domain.Entities;
using RunMate.Application.Runs.Dtos;

namespace RunMate.Application.Runs;

/// <summary>
/// Handles business logic relating to runs.
/// </summary>
public interface IRunsService
{
    /// <summary>
    /// Creates a new run.
    /// </summary>
    /// <param name="userId">The ID of the user creating the run.</param>
    /// <param name="runDate">The date the run is scheduled for.</param>
    /// <param name="distanceInKm">The distance of the run.</param>
    /// <param name="avgPace">The average pace of the runners during the run.</param>
    /// <returns>The newly created run.</returns>
    Task<Run> CreateRunAsync(Guid userId, DateTime runDate, double distanceInKm, TimeSpan avgPace);

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
    /// <returns>A run corresponding to the given ID.</returns>
    Task<Run> GetRunByIdAsync(Guid runId);

    /// <summary>
    /// Gets all runs for a given user.
    /// </summary>
    /// <param name="userId">The ID of the user to get runs for.</param>
    /// <returns>A collection of runs belonging to the user.</returns>
    Task<IEnumerable<Run>> GetRunsByUserIdAsync(Guid userId);

    /// <summary>
    /// Updates an existing run with updated field values.
    /// </summary>
    /// <param name="currentUserId">The ID of the current logged in user.</param>
    /// <param name="runId">The ID of the run to update.</param>
    /// <param name="runDate">The date the run is scheduled for.</param>
    /// <param name="distanceInKm">The distance of the run.</param>
    /// <param name="avgPace">The average pace of the runners during the run.</param>
    Task UpdateRunAsync(Guid currentUserId, Guid runId, DateTime runDate, double distanceInKm, TimeSpan avgPace);

    /// <summary>
    /// Deletes a run.
    /// </summary>
    /// <param name="currentUserId">The ID of the current logged in user.</param>
    /// <param name="runId">The ID of the run to delete.</param>
    Task DeleteRunAsync(Guid currentUserId, Guid runId);
}