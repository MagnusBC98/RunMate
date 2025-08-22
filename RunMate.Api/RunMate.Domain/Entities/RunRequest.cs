using System.ComponentModel.DataAnnotations.Schema;
using RunMate.Domain.Enums;

namespace RunMate.Domain.Entities;

[Table("RunRequests")]
public class RunRequest(Guid runId, Guid runOwnerUserId, Guid requesterUserId)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid RunId { get; private set; } = runId;
    public Guid RunOwnerUserId { get; private set; } = runOwnerUserId;
    public Guid RequesterUserId { get; private set; } = requesterUserId;
    public RunRequestStatus Status { get; private set; } = RunRequestStatus.Pending;

    public Run Run { get; private set; } = null!;

    public void UpdateStatus(RunRequestStatus newStatus)
    {
        Status = newStatus;
    }
}