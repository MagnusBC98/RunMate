using RunMate.Domain.Entities;
using RunMate.Application.Runs.Dtos;
using RunMate.Application.RunMates.Dtos;

namespace RunMate.Application.RunRequests;

/// <summary>
/// Handles database operations relating to Run Requests.
/// </summary>
public interface IRunRequestsRepository
{
    /// <summary>
    /// Creates a new Run Request.
    /// </summary>
    /// <param name="request">The Run Request to add.</param>
    /// <returns>The created Run Request.</returns>
    Task<RunRequest> AddRunRequestAsync(RunRequest request);

    /// <summary>
    /// Gets a specific Run Request for a given ID.
    /// </summary>
    /// <param name="requestId">The ID of the Run Request to find.</param>
    /// <returns>A Run Request if one exists for the given ID; otherwise, null.</returns>
    Task<RunRequest?> GetRunRequestByIdAsync(Guid requestId);

    /// <summary>
    /// Gets all pending Run Requests for a given run.
    /// </summary>
    /// <param name="runId">The ID of the run to get requests for.</param>
    /// <returns>A collection of <see cref="GetRunRequestsResult"/> objects.</returns>
    Task<IEnumerable<GetRunRequestsResult>> GetRunRequestsByRunIdAsync(Guid runId);

    /// <summary>
    /// Updates an existing Run Request.
    /// </summary>
    /// <param name="request">The existing Run Request.</param>
    Task UpdateRequestAsync(RunRequest request);

    /// <summary>
    /// Gets all RunMates for a given user.
    /// </summary>
    /// <param name="userId">The ID of the user to get RunMates for.</param>
    /// <returns>A collection of <see cref="RunMateResult"/> objects.</returns>
    Task<IEnumerable<RunMateResult>> GetRunMatesAsync(Guid userId);
}