using RunMate.Domain.Entities;

namespace RunMate.Application.Users;

/// <summary>
/// Handles business logic relating to running stats.
/// </summary>
public interface IStatsService
{
    /// <summary>
    /// Gets running stats for a given user.
    /// </summary>
    /// <param name="userId">The ID of the user to get running stats for.</param>
    /// <returns>The running stats for the given user.</returns>
    Task<RunningStats> GetStatsByUserAsync(Guid userId);

    /// <summary>
    /// Updates user's running stats with new values.
    /// </summary>
    /// <param name="userId">The ID of the user whose stats are being updated.</param>
    /// <param name="FiveKmPb">The Five Km Personal Best value.</param>
    /// <param name="TenKmPb">The Ten Km Personal Best value.</param>
    /// <param name="HalfMarathonPb"><The Half Marathon Personal Best value./param>
    /// <param name="MarathonPb"><The Marathon Personal Best value./param>
    Task UpdateUserStatsAsync(Guid userId, TimeSpan? FiveKmPb, TimeSpan? TenKmPb, TimeSpan? HalfMarathonPb, TimeSpan? MarathonPb);
}