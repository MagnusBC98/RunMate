using RunMate.Application.RunMates.Dtos;

namespace RunMate.Application.RunMates;

public interface IRunMatesService
{
    Task<IEnumerable<RunMateResult>> GetRunMatesAsync(Guid userId);
}