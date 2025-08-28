using RunMate.Domain.Entities;
using RunMate.Application.Runs;

namespace RunMate.Application.RunRequests;

public interface IRunRequestsService
{
    Task<RunRequest> AddRunRequestAsync(Guid runId, Guid requesterUserId);
    Task<RunRequest> GetRunRequestByIdAsync(Guid requestId);
    Task<IEnumerable<GetRunRequestsResult>> GetRunRequestsByRunIdAsync(Guid currentUserId, Guid runId);
    Task<RunRequest> UpdateRequestStatusAsync(Guid currentUserId, Guid requestId, string newStatus);
}