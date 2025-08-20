using RunMate.Application.Interfaces;
using RunMate.Domain.Entities;

namespace RunMate.Application.Services;

public class RunsService : IRunsService
{
    private readonly IRunsRepository _runsRepository;

    public RunsService(IRunsRepository runsRepository)
    {
        _runsRepository = runsRepository;
    }

    public async Task<Run> CreateRunAsync(Guid userId, DateTime runDate, double distanceInKm, TimeSpan avgPace)
    {
        if (distanceInKm <= 0)
        {
            throw new InvalidOperationException("Distance must be greater than zero.");
        }

        var run = new Run(userId, runDate, distanceInKm, avgPace);
        return await _runsRepository.AddRunAsync(run);
    }

    public async Task<ICollection<Run>> SearchRunsAsync(double? minDistanceKm, double? maxDistanceKm, TimeSpan? minPace, TimeSpan? maxPace)
    {
        return await _runsRepository.SearchRunsAsync(minDistanceKm, maxDistanceKm, minPace, maxPace);
    }

    public async Task<Run> GetRunByIdAsync(Guid runId)
    {
        return await _runsRepository.GetRunByIdAsync(runId);
    }

    public async Task UpdateRunAsync(Guid runId, DateTime runDate, double distanceInKm, TimeSpan avgPace)
    {
        var run = await _runsRepository.GetRunByIdAsync(runId) ?? throw new InvalidOperationException("Run not found.");
        run.UpdateRun(runDate, distanceInKm, avgPace);

        await _runsRepository.UpdateRunAsync(run);
    }

    public async Task DeleteRunAsync(Guid runId)
    {
        var run = await _runsRepository.GetRunByIdAsync(runId) ?? throw new InvalidOperationException("Run not found.");
        await _runsRepository.DeleteRunAsync(run);
    }
}