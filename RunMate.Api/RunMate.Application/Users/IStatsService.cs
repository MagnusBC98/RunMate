using RunMate.Domain.Entities;

namespace RunMate.Application.Users;

public interface IStatsService
{
    Task<RunningStats> GetStatsByUserAsync(Guid userId);
    Task UpdateUserStatsAsync(Guid userId, TimeSpan? FiveKmPb, TimeSpan? TenKmPb, TimeSpan? HalfMarathonPb, TimeSpan? MarathonPb);
}