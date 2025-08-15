using RunMate.Application.Interfaces;
using RunMate.Domain.Entities;

namespace RunMate.Application.Services;

public class AuthService : IAuthService
{
    private IUserRepository _userRepository;
    private IStatsRepository _statsRepository;

    public AuthService(IUserRepository userRepository, IStatsRepository statsRepository)
    {
        _userRepository = userRepository;
        _statsRepository = statsRepository;
    }
    public async Task<User> RegisterUserAsync(string firstName, string lastName, string email, string password)
    {
        var userToCreate = new User(firstName, lastName, email);

        var createdUser = await _userRepository.AddUserAsync(userToCreate, password);

        var stats = new RunningStats(createdUser.Id);
        await _statsRepository.AddStatsAsync(stats);

        return createdUser;
    }

}