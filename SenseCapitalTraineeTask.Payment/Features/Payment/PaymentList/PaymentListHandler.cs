using JetBrains.Annotations;
using MediatR;
using SenseCapitalTraineeTask.Payment.Features.Payment.Data;

namespace SenseCapitalTraineeTask.Payment.Features.Payment.PaymentList;

[UsedImplicitly]
public class PaymentListHandler : IRequestHandler<PaymentListQuery, List<PaymentOperation>>
{
    private readonly PaymentData _paymentData;

    public PaymentListHandler(PaymentData paymentData)
    {
        _paymentData = paymentData;
    }
    
    public Task<List<PaymentOperation>> Handle(PaymentListQuery request, CancellationToken cancellationToken)
    {
        var response = _paymentData.Get();
        
        return Task.FromResult(response);
    }
}