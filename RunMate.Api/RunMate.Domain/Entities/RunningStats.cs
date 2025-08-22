namespace RunMate.Domain.Entities;

public class RunningStats(Guid userId)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid UserId { get; private set; } = userId;
    public TimeSpan? FiveKmPb { get; private set; } = new TimeSpan(0, 0, 0);
    public TimeSpan? TenKmPb { get; private set; } = new TimeSpan(0, 0, 0);
    public TimeSpan? HalfMarathonPb { get; private set; } = new TimeSpan(0, 0, 0);
    public TimeSpan? MarathonPb { get; private set; } = new TimeSpan(0, 0, 0);

    public void UpdateStats(TimeSpan? fiveKmPb, TimeSpan? tenKmPb, TimeSpan? halfMarathonPb, TimeSpan? marathonPb)
    {
        FiveKmPb = fiveKmPb;
        TenKmPb = tenKmPb;
        HalfMarathonPb = halfMarathonPb;
        MarathonPb = marathonPb;
    }
}