namespace SenseCapitalTraineeTask.Payment.Data;

public class PaymentData
{
    private readonly List<PaymentOperation> _paymentOperations;

    public PaymentData()
    {
        _paymentOperations = new List<PaymentOperation>();
    }
    
    public PaymentOperation Create(PaymentOperation paymentOperation)
    {
        paymentOperation.Id = Guid.NewGuid();
        paymentOperation.CreatedAt = DateTime.Now;
        paymentOperation.State = PaymentState.Hold;
        
        _paymentOperations.Add(paymentOperation);

        return paymentOperation;
    }

    public PaymentOperation UpdateStatus(PaymentOperation paymentOperation)
    {
        var index = _paymentOperations.IndexOf(paymentOperation);

        paymentOperation.CanceledAt = paymentOperation.State == PaymentState.Canceled ? DateTime.Now : null;
        paymentOperation.ConfirmedAt = paymentOperation.State == PaymentState.Confirmed ? DateTime.Now : null;
        
        _paymentOperations[index] = paymentOperation;

        return paymentOperation;
    }

    public List<PaymentOperation> Get()
    {
        return _paymentOperations;
    }

    public PaymentOperation? Get(Guid guid)
    {
        return _paymentOperations.FirstOrDefault(p => p.Id == guid);
    }
}