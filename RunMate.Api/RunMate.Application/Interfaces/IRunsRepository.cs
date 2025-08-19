using RunMate.Domain.Entities;

namespace RunMate.Application.Interfaces;

public interface IRunsRepository
{
    Task<Run> AddRunAsync(Run run);
}