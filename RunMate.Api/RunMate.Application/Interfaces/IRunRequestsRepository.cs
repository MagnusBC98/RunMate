using RunMate.Domain.Entities;

namespace RunMate.Application.Interfaces;

public interface IRunRequestsRepository
{
    Task<RunRequest> AddRunRequestAsync(RunRequest request);
    Task<RunRequest> GetRunRequestByIdAsync(Guid requestId);
}