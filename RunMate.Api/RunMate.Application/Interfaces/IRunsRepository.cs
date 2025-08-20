using RunMate.Domain.Entities;

namespace RunMate.Application.Interfaces;

public interface IRunsRepository
{
    Task<Run> AddRunAsync(Run run);
    Task<ICollection<Run>> SearchRunsAsync(double? minDistanceKm, double? maxDistanceKm, TimeSpan? minPace, TimeSpan? maxPace);
}