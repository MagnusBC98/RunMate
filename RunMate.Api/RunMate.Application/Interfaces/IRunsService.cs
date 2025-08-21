using RunMate.Domain.Entities;

namespace RunMate.Application.Interfaces;

public interface IRunsService
{
    Task<Run> CreateRunAsync(Guid userId, DateTime runDate, double distanceInKm, TimeSpan avgPace);
    Task<IEnumerable<Run>> SearchRunsAsync(double? minDistanceKm, double? maxDistanceKm, TimeSpan? minPace, TimeSpan? maxPace);
    Task<Run> GetRunByIdAsync(Guid runId);
    Task UpdateRunAsync(Guid runId, DateTime runDate, double distanceInKm, TimeSpan avgPace);
    Task DeleteRunAsync(Guid runId);
}