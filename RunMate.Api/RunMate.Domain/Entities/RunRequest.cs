using System.ComponentModel.DataAnnotations.Schema;
using RunMate.Domain.Enums;

namespace RunMate.Domain.Entities;

/// <summary>
/// Represents a request made by a user to another user's run.
/// </summary>
[Table("RunRequests")]
public class RunRequest(Guid runId, Guid runOwnerUserId, Guid requesterUserId)
{
    /// <summary>
    /// The unique identifier for the request.
    /// </summary>
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// The ID of the run the request is being made to.
    /// </summary>
    public Guid RunId { get; private set; } = runId;

    /// <summary>
    /// The ID of the user who owns the run the request is being made to.
    /// </summary>
    public Guid RunOwnerUserId { get; private set; } = runOwnerUserId;

    /// <summary>
    /// The ID of the user making the request.
    /// </summary>
    public Guid RequesterUserId { get; private set; } = requesterUserId;

    /// <summary>
    /// The status of the request (Accepted, Declined or Pending).
    /// </summary>
    public RunRequestStatus Status { get; private set; } = RunRequestStatus.Pending;

    /// <summary>
    /// Represents the run the request is being made to.
    /// </summary>
    public Run Run { get; private set; } = null!;

    public void UpdateStatus(RunRequestStatus newStatus)
    {
        Status = newStatus;
    }
}