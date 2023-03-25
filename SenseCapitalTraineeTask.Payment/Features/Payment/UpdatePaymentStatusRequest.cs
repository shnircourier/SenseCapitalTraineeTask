using SenseCapitalTraineeTask.Payment.Data;

namespace SenseCapitalTraineeTask.Payment.Features.Payment;

public class UpdatePaymentStatusRequest
{
    public Guid Id { get; set; }
    
    public PaymentState State { get; set; }
    
    public string? Description { get; set; }
}