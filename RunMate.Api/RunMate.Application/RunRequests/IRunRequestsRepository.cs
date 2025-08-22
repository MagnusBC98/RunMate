using RunMate.Domain.Entities;

namespace RunMate.Application.RunRequests;

public interface IRunRequestsRepository
{
    Task<RunRequest> AddRunRequestAsync(RunRequest request);
    Task<RunRequest?> GetRunRequestByIdAsync(Guid requestId);
    Task<IEnumerable<RunRequest>> GetRunRequestsByRunIdAsync(Guid runId);
    Task UpdateRequestAsync(RunRequest request);
}