using Microsoft.Extensions.DependencyInjection;
using RunMate.Application.Services;
using RunMate.Application.Interfaces;

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

        return services;
    }
}