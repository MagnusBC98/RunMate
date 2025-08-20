using RunMate.Domain.Entities;

namespace RunMate.Application.Interfaces;

public interface IRunRequestsService
{
    Task<RunRequest> AddRunRequestAsync(Guid runId, Guid requesterUserId);
}