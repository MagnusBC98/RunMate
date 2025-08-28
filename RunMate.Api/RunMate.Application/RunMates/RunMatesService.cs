using RunMate.Application.RunMates.Dtos;
using RunMate.Application.RunRequests;

namespace RunMate.Application.RunMates;

public class RunMatesService(IRunRequestsRepository runRequestsRepository) : IRunMatesService
{

    private readonly IRunRequestsRepository _runRequestsRepository = runRequestsRepository;

    public async Task<IEnumerable<RunMateResult>> GetRunMatesAsync(Guid userId)
    {
        return await _runRequestsRepository.GetRunMatesAsync(userId);
    }
}