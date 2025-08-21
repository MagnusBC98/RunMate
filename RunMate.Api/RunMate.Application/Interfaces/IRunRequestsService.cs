using RunMate.Domain.Entities;

namespace RunMate.Application.Interfaces;

public interface IRunRequestsService
{
    Task<RunRequest> AddRunRequestAsync(Guid runId, Guid requesterUserId);
    Task<RunRequest> GetRunRequestByIdAsync(Guid requestId);
    Task<IEnumerable<RunRequest>> GetRunRequestsByRunIdAsync(Guid runId);
    Task<RunRequest> UpdateRequestStatusAsync(Guid requestId, string newStatus);
}