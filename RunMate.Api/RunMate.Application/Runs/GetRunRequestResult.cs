namespace RunMate.Application.Runs;

public record GetRunRequestsResult(
    Guid Id,
    string Status,
    string RequesterFirstName,
    string RequesterLastName,
    Guid RequesterUserId
);