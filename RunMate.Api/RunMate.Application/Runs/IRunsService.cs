using RunMate.Domain.Entities;

namespace RunMate.Application.Runs;

public interface IRunsService
{
    Task<Run> CreateRunAsync(Guid userId, DateTime runDate, double distanceInKm, TimeSpan avgPace);
    Task<IEnumerable<Run>> SearchRunsAsync(double? minDistanceKm, double? maxDistanceKm, TimeSpan? minPace, TimeSpan? maxPace);
    Task<Run> GetRunByIdAsync(Guid runId);
    Task UpdateRunAsync(Guid currentUserId, Guid runId, DateTime runDate, double distanceInKm, TimeSpan avgPace);
    Task DeleteRunAsync(Guid currentUserId, Guid runId);
}