namespace RunMate.Application.RunMates.Dtos;

/// <summary>
/// Represents the object returned by a call to retrieve Run Mates.
/// </summary>
/// <param name="RunId">The Run ID of the matched run.</param>
/// <param name="RunDate">The Date of the matched run.</param>
/// <param name="RunMateUserName">The name of the matched user.</param>
/// <param name="RunMateUserId">The ID of the matched user.</param>
/// <param name="RequestId">The ID of the accepted request.</param>
public record RunMateResult(
    Guid RunId,
    DateTime RunDate,
    string RunMateUserName,
    Guid RunMateUserId,
    Guid RequestId
);