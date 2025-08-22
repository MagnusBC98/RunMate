using RunMate.Application.Exceptions;
using RunMate.Domain.Entities;

namespace RunMate.Application.Users;

public class StatsService : IStatsService
{
    private IStatsRepository _statsRepository;
    public StatsService(IStatsRepository statsRepository)
    {
        _statsRepository = statsRepository;
    }

    public async Task<RunningStats> GetStatsByUserAsync(Guid userId)
    {
        var stats = await _statsRepository.GetStatsByUserAsync(userId);

        if (stats == null)
        {
            throw new NotFoundException($"Stats for user with ID {userId} not found.");
        }

        return stats;
    }

    public async Task UpdateUserStatsAsync(Guid userId, TimeSpan? FiveKmPb, TimeSpan? TenKmPb,
        TimeSpan? HalfMarathonPb, TimeSpan? MarathonPb)
    {
        var existingStats = await _statsRepository.GetStatsByUserAsync(userId);

        if (existingStats is null)
        {
            throw new NotFoundException($"Stats for user with ID {userId} not found.");
        }

        existingStats.UpdateStats(FiveKmPb, TenKmPb, HalfMarathonPb, MarathonPb);

        await _statsRepository.UpdateUserStatsAsync(existingStats);
    }
}