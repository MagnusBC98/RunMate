namespace RunMate.Endpoints.Dtos;

public record CreateRunDto(
    DateTime RunDate,
    double DistanceInKm,
    TimeSpan AvgPaceInMinutesPerKm
);