using RunMate.Domain.Entities;

namespace RunMate.Application.Interfaces;

public interface IUserRepository
{
    Task<User> AddUserAsync(User user, string password);
    Task<User> GetUserByIdAsync(Guid userId);
    Task UpdateUserAsync(User user);
    Task<User?> GetUserByCredentialsAsync(string email, string password);
}