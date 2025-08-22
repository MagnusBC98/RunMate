using RunMate.Domain.Entities;

namespace RunMate.Application.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}