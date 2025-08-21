namespace RunMate.Endpoints.Dtos;

public record CreateRunDto(
    Guid UserId,
    DateTime RunDate,
    double DistanceInKm,
    TimeSpan AvgPaceInMinutesPerKm
);