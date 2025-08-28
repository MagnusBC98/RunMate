using RunMate.Domain.Entities;
using RunMate.Application.Runs;
using RunMate.Application.RunMates.Dtos;

namespace RunMate.Application.RunRequests;

public interface IRunRequestsRepository
{
    Task<RunRequest> AddRunRequestAsync(RunRequest request);
    Task<RunRequest?> GetRunRequestByIdAsync(Guid requestId);
    Task<IEnumerable<GetRunRequestsResult>> GetRunRequestsByRunIdAsync(Guid runId);
    Task UpdateRequestAsync(RunRequest request);
    Task<IEnumerable<RunMateResult>> GetRunMatesAsync(Guid userId);
}