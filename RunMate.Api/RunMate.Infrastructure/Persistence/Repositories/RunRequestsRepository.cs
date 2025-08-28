using Microsoft.EntityFrameworkCore;
using RunMate.Application.RunRequests;
using RunMate.Domain.Entities;
using RunMate.Application.Runs;
using RunMate.Domain.Enums;
using RunMate.Application.RunMates.Dtos;

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

    public async Task<IEnumerable<GetRunRequestsResult>> GetRunRequestsByRunIdAsync(Guid runId)
    {
        var requests = await _context.RunRequests
        .Where(rr => rr.RunId == runId && rr.Status == RunRequestStatus.Pending)
        .Join(
            _context.Users,
            runRequest => runRequest.RequesterUserId,
            user => user.Id,
            (runRequest, user) => new GetRunRequestsResult(
                runRequest.Id,
                runRequest.Status.ToString(),
                user.FirstName ?? string.Empty,
                user.LastName ?? string.Empty,
                user.Id
            ))
        .ToListAsync();

        return requests;
    }

    public async Task UpdateRequestAsync(RunRequest request)
    {
        _context.RunRequests.Update(request);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<RunMateResult>> GetRunMatesAsync(Guid userId)
    {
        var requestsAsOwner = await _context.RunRequests
            .Where(rr => rr.RunOwnerUserId == userId && rr.Status == RunRequestStatus.Accepted)
            .Join(
                _context.Users,
                request => request.RequesterUserId,
                user => user.Id,
                (request, user) => new RunMateResult(
                    request.RunId,
                    request.Run.RunDate,
                    $"{user.FirstName} {user.LastName}",
                    user.Id,
                    request.Id
                ))
            .ToListAsync();

        var requestsAsRequester = await _context.RunRequests
            .Where(rr => rr.RequesterUserId == userId && rr.Status == RunRequestStatus.Accepted)
            .Join(
                _context.Users,
                request => request.RunOwnerUserId,
                user => user.Id,
                (request, user) => new RunMateResult(
                    request.RunId,
                    request.Run.RunDate,
                    $"{user.FirstName} {user.LastName}",
                    user.Id,
                    request.Id
                ))
            .ToListAsync();

        return requestsAsOwner.Concat(requestsAsRequester);
    }
}