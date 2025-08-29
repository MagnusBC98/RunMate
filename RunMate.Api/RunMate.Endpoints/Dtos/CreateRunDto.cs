namespace RunMate.Endpoints.Dtos;

/// <summary>
/// Represents the data required to create a new run.
/// </summary>
/// <param name="RunDate">The date the run is scheduled for.</param>
/// <param name="DistanceInKm">The total distance of the run.</param>
/// <param name="AvgPaceInMinutesPerKm">The average pace of of the run.</param>
public record CreateRunDto(
    DateTime RunDate,
    double DistanceInKm,
    TimeSpan AvgPaceInMinutesPerKm
);