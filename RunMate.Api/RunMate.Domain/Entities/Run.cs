using System.ComponentModel.DataAnnotations.Schema;

namespace RunMate.Domain.Entities;

[Table("Runs")]
public class Run(Guid userId, DateTime runDate, double distanceInKm, TimeSpan avgPaceInMinutesPerKm)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid UserId { get; private set; } = userId;
    public DateTime RunDate { get; private set; } = runDate;
    public double DistanceInKm { get; private set; } = distanceInKm;
    public TimeSpan AvgPaceInMinutesPerKm { get; private set; } = avgPaceInMinutesPerKm;

    public void UpdateRun(DateTime runDate, double distanceInKm, TimeSpan avgPaceInMinutesPerKm)
    {
        RunDate = runDate;
        DistanceInKm = distanceInKm;
        AvgPaceInMinutesPerKm = avgPaceInMinutesPerKm;
    }
}