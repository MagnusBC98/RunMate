using RunMate.Application.Users;
using RunMate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace RunMate.Infrastructure.Persistence.Repositories;

public class StatsRepository : IStatsRepository
{
    private readonly ApplicationDbContext _context;

    public StatsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddStatsAsync(RunningStats stats)
    {
        _context.RunningStats.Add(stats);
        await _context.SaveChangesAsync();
    }

    public async Task<RunningStats?> GetStatsByUserAsync(Guid userId)
    {
        return await _context.RunningStats.FirstOrDefaultAsync(s => s.UserId == userId);
    }

    public async Task UpdateUserStatsAsync(RunningStats stats)
    {
        _context.RunningStats.Update(stats);
        await _context.SaveChangesAsync();
    }
}