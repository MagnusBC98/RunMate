using RunMate.Domain.Entities;
using RunMate.Application.Runs.Dtos;

namespace RunMate.Application.RunRequests;

/// <summary>
/// Handles business logic relating to Run Requests.
/// </summary>
public interface IRunRequestsService
{
    /// <summary>
    /// Creates a new run request.
    /// </summary>
    /// <param name="runId">The ID of the run the request is being made for.</param>
    /// <param name="requesterUserId">The ID of the user making the request.</param>
    /// <returns>The newly created run request.</returns>
    Task<RunRequest> AddRunRequestAsync(Guid runId, Guid requesterUserId);

    /// <summary>
    /// Gets a specific run request for a given ID.
    /// </summary>
    /// <param name="requestId">The ID of the request to find.</param>
    /// <returns>The run request corresponding to the ID.</returns>
    Task<RunRequest> GetRunRequestByIdAsync(Guid requestId);

    /// <summary>
    /// Gets run requests for a given run.
    /// </summary>
    /// <param name="currentUserId">The ID of the current logged in user.</param>
    /// <param name="runId">The ID of the run to get requests for.</param>
    /// <returns>A collection of <see cref="GetRunRequestsResult"/> objects.</returns>
    Task<IEnumerable<GetRunRequestsResult>> GetRunRequestsByRunIdAsync(Guid currentUserId, Guid runId);

    /// <summary>
    /// Updates an existing request with a new status.
    /// </summary>
    /// <param name="currentUserId">The ID of the current logged in user.</param>
    /// <param name="requestId">The ID of the request to update.</param>
    /// <param name="newStatus">The new status to assign to the request.</param>
    /// <returns>The updated run request.</returns>
    Task<RunRequest> UpdateRequestStatusAsync(Guid currentUserId, Guid requestId, string newStatus);
}