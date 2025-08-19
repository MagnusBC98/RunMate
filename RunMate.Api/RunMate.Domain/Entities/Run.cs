using System.ComponentModel.DataAnnotations.Schema;

namespace RunMate.Domain.Entities;

[Table("Runs")]
public class Run
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime RunDate { get; private set; }
    public double DistanceInKm { get; private set; }
    public TimeSpan AvgPaceInMinutesPerKm { get; private set; }

    public User User { get; private set; } = null!;
}