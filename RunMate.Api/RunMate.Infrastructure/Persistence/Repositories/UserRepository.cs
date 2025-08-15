using Microsoft.AspNetCore.Identity;
using RunMate.Application.Interfaces;
using RunMate.Domain.Entities;
using RunMate.Infrastructure.Identity;

namespace RunMate.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserRepository(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<User> AddUserAsync(User user, string password)
    {
        var applicationUser = new ApplicationUser
        {
            UserName = user.Email,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName
        };

        var result = await _userManager.CreateAsync(applicationUser, password);

        if (!result.Succeeded)
        {
            var firstError = result.Errors.FirstOrDefault()?.Description ?? "Unknown error.";
            throw new InvalidOperationException($"Failed to create user: {firstError}");
        }

        return new User(
            applicationUser.Id,
            applicationUser.FirstName,
            applicationUser.LastName,
            applicationUser.Email
        );
    }

    public async Task<User> GetUserByIdAsync(Guid userId)
    {
        var applicationUser = await _userManager.FindByIdAsync(userId.ToString());

        if (applicationUser is null)
        {
            return null;
        }

        return new User(applicationUser.Id, applicationUser.FirstName, applicationUser.LastName, applicationUser.Email);
    }

    public async Task UpdateUserAsync(User user)
    {
        var applicationUser = await _userManager.FindByIdAsync(user.Id.ToString());

        if (applicationUser is not null)
        {
            applicationUser.FirstName = user.FirstName;
            applicationUser.LastName = user.LastName;

            await _userManager.UpdateAsync(applicationUser);
        }
    }
}