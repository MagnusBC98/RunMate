namespace RunMate.Domain.Entities;

public class RunningStats
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public TimeSpan? FiveKmPb { get; private set; }
    public TimeSpan? TenKmPb { get; private set; }
    public TimeSpan? HalfMarathonPb { get; private set; }
    public TimeSpan? MarathonPb { get; private set; }

    public RunningStats(Guid userId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        FiveKmPb = new TimeSpan(0, 0, 0);
        TenKmPb = new TimeSpan(0, 0, 0);
        HalfMarathonPb = new TimeSpan(0, 0, 0);
        MarathonPb = new TimeSpan(0, 0, 0);
    }

    public void UpdateStats(TimeSpan? fiveKmPb, TimeSpan? tenKmPb, TimeSpan? halfMarathonPb, TimeSpan? marathonPb)
    {
        FiveKmPb = fiveKmPb;
        TenKmPb = tenKmPb;
        HalfMarathonPb = halfMarathonPb;
        MarathonPb = marathonPb;
    }
}