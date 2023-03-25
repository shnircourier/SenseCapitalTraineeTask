using JetBrains.Annotations;

namespace SenseCapitalTraineeTask.Payment.Features.Payment;

public class PaymentCreateRequest
{
    public string? Description { get; [UsedImplicitly] set; }
}