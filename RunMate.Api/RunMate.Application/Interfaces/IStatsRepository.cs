using RunMate.Domain.Entities;

namespace RunMate.Application.Interfaces;

public interface IStatsRepository
{
    Task AddStatsAsync(RunningStats stats);
    Task<RunningStats?> GetStatsByUserAsync(Guid userId);
    Task UpdateUserStatsAsync(RunningStats stats);
}