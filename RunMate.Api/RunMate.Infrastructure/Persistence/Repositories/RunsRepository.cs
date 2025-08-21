using RunMate.Application.Interfaces;
using RunMate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace RunMate.Infrastructure.Persistence.Repositories;

public class RunsRepository : IRunsRepository
{
    private readonly ApplicationDbContext _context;

    public RunsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Run> AddRunAsync(Run run)
    {
        _context.Runs.Add(run);
        await _context.SaveChangesAsync();
        return run;
    }

    public async Task<IEnumerable<Run>> SearchRunsAsync(double? minDistanceKm, double? maxDistanceKm, TimeSpan? minPace, TimeSpan? maxPace)
    {
        var query = _context.Runs.AsQueryable();

        if (minDistanceKm.HasValue)
        {
            query = query.Where(run => run.DistanceInKm >= minDistanceKm.Value);
        }

        if (maxDistanceKm.HasValue)
        {
            query = query.Where(run => run.DistanceInKm <= maxDistanceKm.Value);
        }

        if (minPace.HasValue)
        {
            query = query.Where(run => run.AvgPaceInMinutesPerKm >= minPace.Value);
        }

        if (maxPace.HasValue)
        {
            query = query.Where(run => run.AvgPaceInMinutesPerKm <= maxPace.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<Run?> GetRunByIdAsync(Guid runId)
    {
        return await _context.Runs.FindAsync(runId);
    }

    public async Task UpdateRunAsync(Run run)
    {
        _context.Runs.Update(run);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRunAsync(Run run)
    {
        if (run != null)
        {
            _context.Runs.Remove(run);
            await _context.SaveChangesAsync();
        }
    }
}