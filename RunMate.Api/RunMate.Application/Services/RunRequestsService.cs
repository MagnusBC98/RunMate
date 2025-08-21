using RunMate.Application.Interfaces;
using RunMate.Domain.Entities;

namespace RunMate.Application.Services;

public class RunRequestsService : IRunRequestsService
{
    private readonly IRunRequestsRepository _runRequestsRepository;
    private readonly IRunsRepository _runsRepository;

    public RunRequestsService(IRunRequestsRepository runRequestsRepository, IRunsRepository runsRepository)
    {
        _runRequestsRepository = runRequestsRepository;
        _runsRepository = runsRepository;
    }

    public async Task<RunRequest> AddRunRequestAsync(Guid runId, Guid requesterUserId)
    {
        var run = await _runsRepository.GetRunByIdAsync(runId);
        var runRequest = new RunRequest(run.Id, run.UserId, requesterUserId);
        return await _runRequestsRepository.AddRunRequestAsync(runRequest);
    }

    public async Task<RunRequest> GetRunRequestByIdAsync(Guid requestId)
    {
        return await _runRequestsRepository.GetRunRequestByIdAsync(requestId);
    }

    public async Task<IEnumerable<RunRequest>> GetRunRequestsByRunIdAsync(Guid runId)
    {
        return await _runRequestsRepository.GetRunRequestsByRunIdAsync(runId);
    }

    public async Task UpdateRequestStatusAsync(Guid requestId, string newStatus)
    {
        var runRequest = await _runRequestsRepository.GetRunRequestByIdAsync(requestId);
        if (Enum.TryParse<RunRequestStatus>(newStatus, true, out var statusEnum))
        {
            runRequest.UpdateStatus(statusEnum);
            await _runRequestsRepository.UpdateRequestAsync(runRequest);
        }
    }
}