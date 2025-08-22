using Microsoft.EntityFrameworkCore;
using RunMate.Application.Exceptions;
using RunMate.Application.RunRequests;
using RunMate.Domain.Entities;

namespace RunMate.Infrastructure.Persistence.Repositories;

public class RunRequestsRepository(ApplicationDbContext context) : IRunRequestsRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<RunRequest> AddRunRequestAsync(RunRequest request)
    {
        _context.RunRequests.Add(request);
        await _context.SaveChangesAsync();
        return request;
    }

    public async Task<RunRequest?> GetRunRequestByIdAsync(Guid requestId)
    {
        return await _context.RunRequests.FindAsync(requestId);
    }

    public async Task<IEnumerable<RunRequest>> GetRunRequestsByRunIdAsync(Guid runId)
    {
        var runRequests = _context.RunRequests.Where(rr => rr.RunId == runId);
        return await runRequests.ToListAsync();
    }

    public async Task UpdateRequestAsync(RunRequest request)
    {
        _context.RunRequests.Update(request);
        await _context.SaveChangesAsync();
    }
}