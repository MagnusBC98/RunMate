namespace RunMate.Endpoints.Dtos;

/// <summary>
/// Represents the running statistics data that can be updated.
/// </summary>
/// <param name="FiveKmPb">The user's five kilometre personal best time.</param>
/// <param name="TenKmPb">The user's ten kilometre personal best time.</param>
/// <param name="HalfMarathonPb">The user's half marathon personal best time.</param>
/// <param name="MarathonPb">The user's marathon personal best time.</param>
public record UpdateStatsDto(TimeSpan? FiveKmPb, TimeSpan? TenKmPb, TimeSpan? HalfMarathonPb, TimeSpan? MarathonPb);