using Microsoft.Extensions.DependencyInjection;
using RunMate.Application.Users;
using RunMate.Application.Authentication;
using RunMate.Application.Runs;
using RunMate.Application.RunRequests;
using RunMate.Application.RunMates;

namespace RunMate.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IStatsService, StatsService>();
        services.AddScoped<IRunsService, RunsService>();
        services.AddScoped<IRunRequestsService, RunRequestsService>();
        services.AddScoped<IRunMatesService, RunMatesService>();

        return services;
    }
}