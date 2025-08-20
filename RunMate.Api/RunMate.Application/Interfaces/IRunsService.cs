using RunMate.Domain.Entities;

namespace RunMate.Application.Interfaces;

public interface IRunsService
{
    Task<Run> CreateRunAsync(Guid userId, DateTime runDate, double distanceInKm, TimeSpan avgPace);
    Task<ICollection<Run>> SearchRunsAsync(double? distanceKm, TimeSpan? minPace, TimeSpan? maxPace);
}