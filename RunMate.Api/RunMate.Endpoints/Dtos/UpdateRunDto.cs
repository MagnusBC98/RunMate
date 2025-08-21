namespace RunMate.Endpoints.Dtos;

public record UpdateRunDto(
    DateTime RunDate,
    double DistanceInKm,
    TimeSpan AvgPaceInMinutesPerKm
);