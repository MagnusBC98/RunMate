namespace RunMate.Endpoints.Dtos;

public record UpdateStatsDto(TimeSpan FiveKmPb, TimeSpan TenKmPb, TimeSpan HalfMarathonPb, TimeSpan MarathonPb);