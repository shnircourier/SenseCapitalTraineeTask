namespace SenseCapitalTraineeTask.Features.Payments;

public class PaymentOperation
{
    public Guid Id { get; set; }

    public PaymentState State { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public DateTime? ConfirmedAt { get; set; }

    public DateTime? CanceledAt { get; set; }

    public string? Description { get; set; }
}