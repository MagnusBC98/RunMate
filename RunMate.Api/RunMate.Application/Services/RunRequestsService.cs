using RunMate.Application.Exceptions;
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

        if (run == null)
        {
            throw new NotFoundException($"Run with ID {runId} not found.");
        }

        var runRequest = new RunRequest(run.Id, run.UserId, requesterUserId);
        return await _runRequestsRepository.AddRunRequestAsync(runRequest);
    }

    public async Task<RunRequest> GetRunRequestByIdAsync(Guid requestId)
    {
        var runRequest = await GetRunRequestAndEnsureExistsAsync(requestId);
        return runRequest;
    }

    public async Task<IEnumerable<RunRequest>> GetRunRequestsByRunIdAsync(Guid runId)
    {
        return await _runRequestsRepository.GetRunRequestsByRunIdAsync(runId);
    }

    public async Task UpdateRequestStatusAsync(Guid requestId, string newStatus)
    {
        var runRequest = await GetRunRequestAndEnsureExistsAsync(requestId);

        if (Enum.TryParse<RunRequestStatus>(newStatus, true, out var statusEnum))
        {
            runRequest.UpdateStatus(statusEnum);
            await _runRequestsRepository.UpdateRequestAsync(runRequest);
        }
        else
        {
            throw new ValidationException($"Invalid status '{newStatus}' for run request.");
        }
    }

    private async Task<RunRequest> GetRunRequestAndEnsureExistsAsync(Guid requestId)
    {
        var runRequest = await _runRequestsRepository.GetRunRequestByIdAsync(requestId);

        if (runRequest == null)
        {
            throw new NotFoundException($"Run Request with ID {requestId} not found.");
        }

        return runRequest;
    }
}