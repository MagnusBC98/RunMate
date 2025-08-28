namespace RunMate.Application.RunMates.Dtos;

public record RunMateResult(
    Guid RunId,
    DateTime RunDate,
    string RunMateUserName,
    Guid RunMateUserId
);