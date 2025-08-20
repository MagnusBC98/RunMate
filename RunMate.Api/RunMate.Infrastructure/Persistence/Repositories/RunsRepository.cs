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

    public async Task<ICollection<Run>> SearchRunsAsync(double? distanceKm, TimeSpan? minPace, TimeSpan? maxPace)
    {
        var query = _context.Runs.AsQueryable();

        if (distanceKm.HasValue)
        {
            query = query.Where(run => run.DistanceInKm >= distanceKm - 1 && run.DistanceInKm <= distanceKm + 1);
        }

        if (minPace.HasValue)
        {
            query = query.Where(r => r.AvgPaceInMinutesPerKm >= minPace.Value);
        }

        if (maxPace.HasValue)
        {
            query = query.Where(r => r.AvgPaceInMinutesPerKm <= maxPace.Value);
        }

        return await query.ToListAsync();
    }
}