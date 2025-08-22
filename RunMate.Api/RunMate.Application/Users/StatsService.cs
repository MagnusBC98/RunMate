using RunMate.Application.Exceptions;
using RunMate.Domain.Entities;

namespace RunMate.Application.Users;

public class StatsService(IStatsRepository statsRepository) : IStatsService
{
    private readonly IStatsRepository _statsRepository = statsRepository;

    public async Task<RunningStats> GetStatsByUserAsync(Guid userId)
    {
        var stats = await _statsRepository.GetStatsByUserAsync(userId) ??
            throw new NotFoundException($"Stats for user with ID {userId} not found.");
        return stats;
    }

    public async Task UpdateUserStatsAsync(Guid userId, TimeSpan? FiveKmPb, TimeSpan? TenKmPb,
        TimeSpan? HalfMarathonPb, TimeSpan? MarathonPb)
    {
        var existingStats = await _statsRepository.GetStatsByUserAsync(userId) ??
            throw new NotFoundException($"Stats for user with ID {userId} not found.");

        existingStats.UpdateStats(FiveKmPb, TenKmPb, HalfMarathonPb, MarathonPb);
        await _statsRepository.UpdateUserStatsAsync(existingStats);
    }
}