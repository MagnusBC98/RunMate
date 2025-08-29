namespace RunMate.Domain.Entities;

/// <summary>
/// Represents a user's running statistics.
/// </summary>
public class RunningStats(Guid userId)
{
    /// <summary>
    /// The unique identifier for the stats.
    /// </summary>
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// The ID of the user the statistics belong to.
    /// </summary>
    public Guid UserId { get; private set; } = userId;

    /// <summary>
    /// The user's five kilometre personal best time.
    /// </summary>
    public TimeSpan? FiveKmPb { get; private set; } = new TimeSpan(0, 0, 0);

    /// <summary>
    /// The user's ten kilometre personal best time.
    /// </summary>
    public TimeSpan? TenKmPb { get; private set; } = new TimeSpan(0, 0, 0);

    /// <summary>
    /// The user's half marathon personal best time.
    /// </summary>
    public TimeSpan? HalfMarathonPb { get; private set; } = new TimeSpan(0, 0, 0);

    /// <summary>
    /// The user's marathon personal best time.
    /// </summary>
    public TimeSpan? MarathonPb { get; private set; } = new TimeSpan(0, 0, 0);

    public void UpdateStats(TimeSpan? fiveKmPb, TimeSpan? tenKmPb, TimeSpan? halfMarathonPb, TimeSpan? marathonPb)
    {
        FiveKmPb = fiveKmPb;
        TenKmPb = tenKmPb;
        HalfMarathonPb = halfMarathonPb;
        MarathonPb = marathonPb;
    }
}