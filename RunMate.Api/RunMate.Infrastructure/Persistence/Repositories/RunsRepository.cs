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
}