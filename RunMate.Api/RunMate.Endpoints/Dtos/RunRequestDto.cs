namespace RunMate.Endpoints.Dtos;

public record RunRequestDto(
    Guid Id,
    Guid RequesterUserId,
    string RequesterFirstName,
    string RequesterLastName
);