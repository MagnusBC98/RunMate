using RunMate.Domain.Entities;

namespace RunMate.Application.Interfaces;

public interface IAuthService
{
    Task<User> RegisterUserAsync(string email, string password, string firstName, string lastName);
}