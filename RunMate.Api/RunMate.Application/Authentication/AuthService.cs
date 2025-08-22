using RunMate.Domain.Entities;
using RunMate.Application.Users;

namespace RunMate.Application.Authentication;

public class AuthService : IAuthService
{
    private IUserRepository _userRepository;
    private IStatsRepository _statsRepository;
    private IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(IUserRepository userRepository, IStatsRepository statsRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _statsRepository = statsRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    public async Task<User> RegisterUserAsync(string firstName, string lastName, string email, string password)
    {
        var userToCreate = new User(firstName, lastName, email);

        var createdUser = await _userRepository.AddUserAsync(userToCreate, password);

        var stats = new RunningStats(createdUser.Id);
        await _statsRepository.AddStatsAsync(stats);

        return createdUser;
    }

    public async Task<string?> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetUserByCredentialsAsync(email, password);

        if (user is null)
        {
            return null;
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return token;
    }

}