using RunMate.Domain.Entities;

namespace RunMate.Application.Runs;

public interface IRunsRepository
{
    Task<Run> AddRunAsync(Run run);
    Task<IEnumerable<SearchRunsResult>> SearchRunsAsync(Guid currentUserId, double? minDistanceKm,
        double? maxDistanceKm, TimeSpan? minPace, TimeSpan? maxPace);
    Task<Run?> GetRunByIdAsync(Guid runId);
    Task<IEnumerable<Run>> GetRunsByUserIdAsync(Guid userId);
    Task UpdateRunAsync(Run run);
    Task DeleteRunAsync(Run run);
}