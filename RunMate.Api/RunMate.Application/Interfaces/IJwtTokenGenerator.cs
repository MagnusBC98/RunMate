using RunMate.Domain.Entities;

namespace RunMate.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}