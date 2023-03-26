using JetBrains.Annotations;

namespace SenseCapitalTraineeTask.Payment.Features.Payment.Data;

public class PaymentOperation
{
    public Guid Id { get; set; }

    public PaymentState State { get; set; }
    
    public DateTime CreatedAt { [UsedImplicitly] get; set; }

    public DateTime? ConfirmedAt { [UsedImplicitly] get; set; }

    public DateTime? CanceledAt { [UsedImplicitly] get; set; }

    public string? Description { [UsedImplicitly] get; set; }
}