namespace SenseCapitalTraineeTask.Features.Payments;

public class UpdatePaymentRequest
{
    public Guid Id { get; set; }
    
    public PaymentState State { get; set; }
    
    public string? Description { get; set; }
}