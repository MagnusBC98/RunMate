using RunMate.Application.Interfaces;
using RunMate.Domain.Entities;

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
        return await _userRepository.GetUserByIdAsync(userId);
    }

    public async Task UpdateUserAsync(Guid userId, string FirstName, string LastName)
    {
        var existingUser = await _userRepository.GetUserByIdAsync(userId);

        if (existingUser is null)
        {
            return;
        }

        existingUser.UpdateProfile(FirstName, LastName);

        await _userRepository.UpdateUserAsync(existingUser);
    }
}