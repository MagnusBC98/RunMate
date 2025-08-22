using RunMate.Domain.Entities;

namespace RunMate.Application.Users;

public interface IStatsRepository
{
    Task AddStatsAsync(RunningStats stats);
    Task<RunningStats?> GetStatsByUserAsync(Guid userId);
    Task UpdateUserStatsAsync(RunningStats stats);
}