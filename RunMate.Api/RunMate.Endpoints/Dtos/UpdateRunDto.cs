namespace RunMate.Endpoints.Dtos;

/// <summary>
/// Represents the run data that can be updated.
/// </summary>
/// <param name="RunDate">The date the run is scheduled for.</param>
/// <param name="DistanceInKm">The total distance of the run.</param>
/// <param name="AvgPaceInMinutesPerKm">The average pace of of the run.</param>
public record UpdateRunDto(
    DateTime RunDate,
    double DistanceInKm,
    TimeSpan AvgPaceInMinutesPerKm
);