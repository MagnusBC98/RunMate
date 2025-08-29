namespace RunMate.Endpoints.Dtos;

/// <summary>
/// Represents the data required to update the status of a run request.
/// </summary>
/// <param name="Status">The updated status of the request.</param>
public record UpdateRunRequestStatusDto(
    string Status
);