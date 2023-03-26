using JetBrains.Annotations;
using SenseCapitalTraineeTask.Payment.Features.Payment.Data;

namespace SenseCapitalTraineeTask.Payment.Features.Payment;

public class UpdatePaymentStatusRequest
{
    public Guid Id { get; [UsedImplicitly] set; }
    
    public PaymentState State { get; [UsedImplicitly] set; }
    
    public string? Description { get; [UsedImplicitly] set; }
}