namespace RunMate.Application.Runs;

public record SearchRunsResult(
     Guid Id,
     DateTime RunDate,
     double DistanceInKm,
     TimeSpan AvgPaceInMinutesPerKm,
     Guid UserId,
     string UserFirstName,
     string UserLastName
);