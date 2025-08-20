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

    public async Task<ICollection<Run>> SearchRunsAsync(double? distanceKm, TimeSpan? minPace, TimeSpan? maxPace)
    {
        return await _runsRepository.SearchRunsAsync(distanceKm, minPace, maxPace);
    }
}