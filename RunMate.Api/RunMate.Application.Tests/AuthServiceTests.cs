using Moq;
using RunMate.Application.Interfaces;
using RunMate.Application.Services;
using RunMate.Domain.Entities;
using Xunit;

namespace RunMate.Application.Tests;

public class AuthServiceTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IStatsRepository> _mockStatsRepository;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockStatsRepository = new Mock<IStatsRepository>();
        _authService = new AuthService(_mockUserRepository.Object, _mockStatsRepository.Object);
    }

    [Fact]
    public async Task RegisterUserAsync_WhenCalled_ShouldCallAddUserAsyncOnRepository()
    {
        var email = "test@test.com";
        var password = "password123";
        var firstName = "Test";
        var lastName = "User";

        _mockUserRepository
            .Setup(repo => repo.AddUserAsync(It.IsAny<User>(), password))
            .ReturnsAsync((User user, string pwd) => new User(Guid.NewGuid(), user.FirstName, user.LastName, user.Email));

        await _authService.RegisterUserAsync(firstName, lastName, email, password);

        _mockUserRepository.Verify(repo => repo.AddUserAsync(It.IsAny<User>(), password), Times.Once);
        _mockStatsRepository.Verify(repo => repo.AddStatsAsync(It.IsAny<RunningStats>()), Times.Once);
    }
}