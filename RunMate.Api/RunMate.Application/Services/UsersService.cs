using RunMate.Application.Interfaces;
using RunMate.Domain.Entities;
using RunMate.Application.Exceptions;

namespace RunMate.Application.Services;

public class UsersService : IUsersService
{
    private IUserRepository _userRepository;

    public UsersService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<User> GetUserByIdAsync(Guid userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user is null)
        {
            throw new NotFoundException($"User with ID {userId} not found.");
        }

        return user;
    }

    public async Task UpdateUserAsync(Guid userId, string FirstName, string LastName)
    {
        var existingUser = await _userRepository.GetUserByIdAsync(userId);

        if (existingUser is null)
        {
            throw new NotFoundException($"User with ID {userId} not found.");
        }

        existingUser.UpdateProfile(FirstName, LastName);

        await _userRepository.UpdateUserAsync(existingUser);
    }
}