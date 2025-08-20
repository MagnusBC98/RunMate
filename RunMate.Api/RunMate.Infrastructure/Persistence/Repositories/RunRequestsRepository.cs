using RunMate.Application.Interfaces;
using RunMate.Domain.Entities;

namespace RunMate.Infrastructure.Persistence.Repositories;

public class RunRequestsRepository : IRunRequestsRepository
{
    private readonly ApplicationDbContext _context;

    public RunRequestsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RunRequest> AddRunRequestAsync(RunRequest request)
    {
        _context.RunRequests.Add(request);
        await _context.SaveChangesAsync();
        return request;
    }
}