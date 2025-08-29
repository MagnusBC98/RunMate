using RunMate.Application.Exceptions;
using RunMate.Application.Users;
using RunMate.Domain.Entities;
using RunMate.Application.Runs.Dtos;

namespace RunMate.Application.Runs;

public class RunsService(
    IRunsRepository runsRepository,
    IUserRepository userRepository) : IRunsService
{
    private readonly IRunsRepository _runsRepository = runsRepository;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Run> CreateRunAsync(Guid userId, DateTime runDate, double distanceInKm, TimeSpan avgPace)
    {
        if (distanceInKm <= 0)
        {
            throw new ValidationException("Distance must be greater than zero.");
        }

        if (await _userRepository.GetUserByIdAsync(userId) == null)
        {
            throw new NotFoundException($"User with ID {userId} not found.");
        }

        var run = new Run(userId, runDate, distanceInKm, avgPace);
        return await _runsRepository.AddRunAsync(run);
    }

    public async Task<IEnumerable<SearchRunsResult>> SearchRunsAsync(Guid currentUserId, double? minDistanceKm,
        double? maxDistanceKm, TimeSpan? minPace, TimeSpan? maxPace)
    {
        return await _runsRepository.SearchRunsAsync(currentUserId, minDistanceKm, maxDistanceKm, minPace, maxPace);
    }

    public async Task<Run> GetRunByIdAsync(Guid runId)
    {
        return await GetRunAndEnsureExistsAsync(runId);
    }

    public async Task<IEnumerable<Run>> GetRunsByUserIdAsync(Guid userId)
    {
        return await _runsRepository.GetRunsByUserIdAsync(userId);
    }

    public async Task UpdateRunAsync(Guid currentUserId, Guid runId, DateTime runDate, double distanceInKm, TimeSpan avgPace)
    {
        if (distanceInKm <= 0)
        {
            throw new ValidationException("Distance must be greater than zero.");
        }

        var run = await GetRunAndEnsureExistsAsync(runId);

        if (run.UserId != currentUserId)
        {
            throw new UnauthorizedException("You are not authorized to update this run.");
        }

        run.UpdateRun(runDate, distanceInKm, avgPace);
        await _runsRepository.UpdateRunAsync(run);
    }

    public async Task DeleteRunAsync(Guid currentUserId, Guid runId)
    {
        var run = await GetRunAndEnsureExistsAsync(runId);

        if (run.UserId != currentUserId)
        {
            throw new UnauthorizedException("You are not authorized to delete this run.");
        }

        await _runsRepository.DeleteRunAsync(run);
    }

    private async Task<Run> GetRunAndEnsureExistsAsync(Guid runId)
    {
        var run = await _runsRepository.GetRunByIdAsync(runId) ??
            throw new NotFoundException($"Run with ID {runId} not found.");
        return run;
    }
}