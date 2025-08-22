using RunMate.Domain.Entities;

namespace RunMate.Application.RunRequests;

public interface IRunRequestsService
{
    Task<RunRequest> AddRunRequestAsync(Guid runId, Guid requesterUserId);
    Task<RunRequest> GetRunRequestByIdAsync(Guid requestId);
    Task<IEnumerable<RunRequest>> GetRunRequestsByRunIdAsync(Guid currentUserId, Guid runId);
    Task<RunRequest> UpdateRequestStatusAsync(Guid currentUserId, Guid requestId, string newStatus);
}