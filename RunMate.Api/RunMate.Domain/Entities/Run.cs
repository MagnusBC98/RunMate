using System.ComponentModel.DataAnnotations.Schema;

namespace RunMate.Domain.Entities;

/// <summary>
/// Represents a running activity posted by a user.
/// </summary>
[Table("Runs")]
public class Run(Guid userId, DateTime runDate, double distanceInKm, TimeSpan avgPaceInMinutesPerKm)
{
    /// <summary>
    /// The unique identifier for the run.
    /// </summary>
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// The ID of the user who owns the run.
    /// </summary>
    public Guid UserId { get; private set; } = userId;

    /// <summary>
    /// The date the run is scheduled for.
    /// </summary>
    public DateTime RunDate { get; private set; } = runDate;

    /// <summary>
    /// The distance of the run.
    /// </summary>
    public double DistanceInKm { get; private set; } = distanceInKm;

    /// <summary>
    /// The average pace of the runners during the run.
    /// </summary>
    public TimeSpan AvgPaceInMinutesPerKm { get; private set; } = avgPaceInMinutesPerKm;

    public void UpdateRun(DateTime runDate, double distanceInKm, TimeSpan avgPaceInMinutesPerKm)
    {
        RunDate = runDate;
        DistanceInKm = distanceInKm;
        AvgPaceInMinutesPerKm = avgPaceInMinutesPerKm;
    }
}