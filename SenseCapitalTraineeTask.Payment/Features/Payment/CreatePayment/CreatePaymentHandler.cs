using JetBrains.Annotations;
using MediatR;
using SenseCapitalTraineeTask.Payment.Features.Payment.Data;

namespace SenseCapitalTraineeTask.Payment.Features.Payment.CreatePayment;

[UsedImplicitly]
public class CreatePaymentHandler : IRequestHandler<CreatePaymentRequest, PaymentOperation>
{
    private readonly PaymentData _paymentData;

    public CreatePaymentHandler(PaymentData paymentData)
    {
        _paymentData = paymentData;
    }
    
    public Task<PaymentOperation> Handle(CreatePaymentRequest request, CancellationToken cancellationToken)
    {
        var response = _paymentData.Create(new PaymentOperation { Description = request.Description });

        return Task.FromResult(response);
    }
}