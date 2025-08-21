using System.ComponentModel.DataAnnotations.Schema;

namespace RunMate.Domain.Entities;

[Table("RunRequests")]
public class RunRequest
{
    public Guid Id { get; private set; }
    public Guid RunId { get; private set; }
    public Guid RunOwnerUserId { get; private set; }
    public Guid RequesterUserId { get; private set; }
    public RunRequestStatus Status { get; private set; }

    public Run Run { get; private set; } = null!;

    public RunRequest(Guid runId, Guid runOwnerUserId, Guid requesterUserId)
    {
        Id = Guid.NewGuid();
        RunId = runId;
        RunOwnerUserId = runOwnerUserId;
        RequesterUserId = requesterUserId;
        Status = RunRequestStatus.Pending;
    }

    public void UpdateStatus(RunRequestStatus newStatus)
    {
        Status = newStatus;
    }
}