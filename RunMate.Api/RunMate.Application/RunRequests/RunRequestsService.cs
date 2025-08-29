using RunMate.Application.Exceptions;
using RunMate.Domain.Entities;
using RunMate.Application.Runs;
using RunMate.Application.Users;
using RunMate.Domain.Enums;
using RunMate.Application.Runs.Dtos;

namespace RunMate.Application.RunRequests;

public class RunRequestsService(
    IRunRequestsRepository runRequestsRepository,
    IRunsRepository runsRepository,
    IUserRepository userRepository) : IRunRequestsService
{
    private readonly IRunRequestsRepository _runRequestsRepository = runRequestsRepository;
    private readonly IRunsRepository _runsRepository = runsRepository;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<RunRequest> AddRunRequestAsync(Guid runId, Guid requesterUserId)
    {
        var run = await _runsRepository.GetRunByIdAsync(runId) ??
            throw new NotFoundException($"Run with ID {runId} not found.");

        if (await _userRepository.GetUserByIdAsync(requesterUserId) == null)
        {
            throw new NotFoundException($"User with ID {requesterUserId} not found.");
        }

        var runRequest = new RunRequest(run.Id, run.UserId, requesterUserId);
        return await _runRequestsRepository.AddRunRequestAsync(runRequest);
    }

    public async Task<RunRequest> GetRunRequestByIdAsync(Guid requestId)
    {
        return await GetRunRequestAndEnsureExistsAsync(requestId);
    }

    public async Task<IEnumerable<GetRunRequestsResult>> GetRunRequestsByRunIdAsync(Guid currentUserId, Guid runId)
    {
        var run = await _runsRepository.GetRunByIdAsync(runId) ??
            throw new NotFoundException($"Run with ID {runId} not found.");

        if (run.UserId != currentUserId)
        {
            throw new UnauthorizedException("You are not authorized to retrieve requests made for this run.");
        }

        return await _runRequestsRepository.GetRunRequestsByRunIdAsync(runId);
    }

    public async Task<RunRequest> UpdateRequestStatusAsync(Guid currentUserId, Guid requestId, string newStatus)
    {
        var runRequest = await GetRunRequestAndEnsureExistsAsync(requestId);

        bool isOwner = runRequest.RunOwnerUserId == currentUserId;
        bool isRequester = runRequest.RequesterUserId == currentUserId;

        if (newStatus.Equals("Accepted", StringComparison.OrdinalIgnoreCase) && !isOwner)
        {
            throw new UnauthorizedException("Only the run owner can accept a request.");
        }

        if (newStatus.Equals("Declined", StringComparison.OrdinalIgnoreCase) && !isOwner && !isRequester)
        {
            throw new UnauthorizedException("You are not authorized to decline this request.");
        }

        if (Enum.TryParse<RunRequestStatus>(newStatus, true, out var statusEnum))
        {
            if (!Enum.IsDefined(typeof(RunRequestStatus), statusEnum))
            {
                throw new ValidationException($"Invalid status '{newStatus}' for run request.");
            }

            runRequest.UpdateStatus(statusEnum);
            await _runRequestsRepository.UpdateRequestAsync(runRequest);
            return runRequest;
        }

        throw new ValidationException($"Invalid status '{newStatus}' for run request.");
    }

    private async Task<RunRequest> GetRunRequestAndEnsureExistsAsync(Guid requestId)
    {
        var runRequest = await _runRequestsRepository.GetRunRequestByIdAsync(requestId) ??
            throw new NotFoundException($"Run Request with ID {requestId} not found.");
        return runRequest;
    }
}