using RunMate.Domain.Entities;
using RunMate.Application.Exceptions;

namespace RunMate.Application.Users;

public class UsersService(IUserRepository userRepository) : IUsersService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<User> GetUserByIdAsync(Guid userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId) ??
            throw new NotFoundException($"User with ID {userId} not found.");
        return user;
    }

    public async Task UpdateUserAsync(Guid userId, string FirstName, string LastName)
    {
        var existingUser = await _userRepository.GetUserByIdAsync(userId) ??
            throw new NotFoundException($"User with ID {userId} not found.");

        existingUser.UpdateProfile(FirstName, LastName);
        await _userRepository.UpdateUserAsync(existingUser);
    }
}