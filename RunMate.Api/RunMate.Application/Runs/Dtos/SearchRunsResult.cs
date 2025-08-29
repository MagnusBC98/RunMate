namespace RunMate.Application.Runs.Dtos;

/// <summary>
/// Represents an individual result from the response for searching for runs.
/// </summary>
/// <param name="Id">The ID of the run.</param>
/// <param name="RunDate">The date the run is scheduled for.</param>
/// <param name="distanceInKm">The distance of the run.</param>
/// <param name="avgPace">The average pace of the runners during the run.</param>
/// <param name="UserId">The ID of the user who owns the run.</param>
/// <param name="UserFirstName">The first name of the user who owns the run.</param>
/// <param name="UserLastName">The last name of the user who owns the run.</param>
public record SearchRunsResult(
     Guid Id,
     DateTime RunDate,
     double DistanceInKm,
     TimeSpan AvgPaceInMinutesPerKm,
     Guid UserId,
     string UserFirstName,
     string UserLastName
);