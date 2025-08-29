namespace RunMate.Application.Runs.Dtos;

/// <summary>
/// Represents the result of a GET request for a Run Request
/// </summary>
/// <param name="Id">The ID of the run request.</param>
/// <param name="Status">The status of the run request.</param>
/// <param name="RequesterFirstName">The first name of the user making the request.</param>
/// <param name="RequesterLastName">The last name of the user making the request.</param>
/// <param name="RequesterUserId">The ID of the user making the request.</param>
public record GetRunRequestsResult(
    Guid Id,
    string Status,
    string RequesterFirstName,
    string RequesterLastName,
    Guid RequesterUserId
);