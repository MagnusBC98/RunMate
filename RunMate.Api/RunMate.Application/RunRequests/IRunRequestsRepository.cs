using RunMate.Domain.Entities;
using RunMate.Application.Runs;

namespace RunMate.Application.RunRequests;

public interface IRunRequestsRepository
{
    Task<RunRequest> AddRunRequestAsync(RunRequest request);
    Task<RunRequest?> GetRunRequestByIdAsync(Guid requestId);
    Task<IEnumerable<GetRunRequestsResult>> GetRunRequestsByRunIdAsync(Guid runId);
    Task UpdateRequestAsync(RunRequest request);
}