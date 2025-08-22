using Microsoft.EntityFrameworkCore;
using RunMate.Application.Exceptions;
using RunMate.Application.RunRequests;
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

    public async Task<RunRequest> GetRunRequestByIdAsync(Guid requestId)
    {
        var runRequest = await _context.RunRequests.FindAsync(requestId);

        if (runRequest == null)
        {
            throw new NotFoundException($"Run Request with ID {requestId} not found.");
        }

        return runRequest;
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