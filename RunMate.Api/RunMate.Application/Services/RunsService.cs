using RunMate.Application.Exceptions;
using RunMate.Application.Interfaces;
using RunMate.Domain.Entities;

namespace RunMate.Application.Services;

public class RunsService : IRunsService
{
    private readonly IRunsRepository _runsRepository;
    private readonly IUserRepository _userRepository;

    public RunsService(IRunsRepository runsRepository, IUserRepository userRepository)
    {
        _runsRepository = runsRepository;
        _userRepository = userRepository;
    }

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

    public async Task<IEnumerable<Run>> SearchRunsAsync(double? minDistanceKm, double? maxDistanceKm, TimeSpan? minPace, TimeSpan? maxPace)
    {
        return await _runsRepository.SearchRunsAsync(minDistanceKm, maxDistanceKm, minPace, maxPace);
    }

    public async Task<Run> GetRunByIdAsync(Guid runId)
    {
        var run = await GetRunAndEnsureExistsAsync(runId);
        return run;
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
        var run = await _runsRepository.GetRunByIdAsync(runId);

        if (run == null)
        {
            throw new NotFoundException($"Run with ID {runId} not found.");
        }

        return run;
    }
}