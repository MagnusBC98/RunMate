using RunMate.Application.Interfaces;
using RunMate.Domain.Entities;

namespace RunMate.Application.Services;

public class StatsService : IStatsService
{
    private IStatsRepository _statsRepository;
    public StatsService(IStatsRepository statsRepository)
    {
        _statsRepository = statsRepository;
    }

    public async Task<RunningStats> GetStatsByUserAsync(Guid userId)
    {
        return await _statsRepository.GetStatsByUserAsync(userId);
    }

    public async Task UpdateUserStatsAsync(Guid userId, TimeSpan? FiveKmPb, TimeSpan? TenKmPb,
        TimeSpan? HalfMarathonPb, TimeSpan? MarathonPb)
    {
        var existingStats = await _statsRepository.GetStatsByUserAsync(userId);

        if (existingStats is null)
        {
            return;
        }

        existingStats.UpdateStats(FiveKmPb, TenKmPb, HalfMarathonPb, MarathonPb);

        await _statsRepository.UpdateUserStatsAsync(existingStats);
    }

}