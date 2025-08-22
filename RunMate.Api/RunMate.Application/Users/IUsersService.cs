using RunMate.Domain.Entities;

namespace RunMate.Application.Users;

public interface IUsersService
{
    Task<User> GetUserByIdAsync(Guid userId);
    Task UpdateUserAsync(Guid userId, string FirstName, string LastName);
}