using JetBrains.Annotations;
using MediatR;
using SenseCapitalTraineeTask.Payment.Data;

namespace SenseCapitalTraineeTask.Payment.Features.Payment.CreatePayment;

[UsedImplicitly]
public class CreatePaymentHandler : IRequestHandler<CreatePaymentCommand, PaymentOperation>
{
    private readonly PaymentData _paymentData;

    public CreatePaymentHandler(PaymentData paymentData)
    {
        _paymentData = paymentData;
    }
    
    public Task<PaymentOperation> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var response = _paymentData.Create(new PaymentOperation { Description = request.Description });

        return Task.FromResult(response);
    }
}