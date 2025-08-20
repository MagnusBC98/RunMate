using RunMate.Domain.Entities;

namespace RunMate.Application.Interfaces;

public interface IRunsRepository
{
    Task<Run> AddRunAsync(Run run);
    Task<ICollection<Run>> SearchRunsAsync(double? distanceKm, TimeSpan? minPace, TimeSpan? maxPace);
}