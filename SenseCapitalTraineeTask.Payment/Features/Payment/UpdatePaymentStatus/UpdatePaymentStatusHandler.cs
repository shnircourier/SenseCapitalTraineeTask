using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.Exceptions;
using SenseCapitalTraineeTask.Payment.Data;

namespace SenseCapitalTraineeTask.Payment.Features.Payment.UpdatePaymentStatus;

[UsedImplicitly]
public class UpdatePaymentStatusHandler : IRequestHandler<UpdatePaymentStatusCommand, PaymentOperation>
{
    private readonly PaymentData _paymentData;

    public UpdatePaymentStatusHandler(PaymentData paymentData)
    {
        _paymentData = paymentData;
    }
    
    public Task<PaymentOperation> Handle(UpdatePaymentStatusCommand request, CancellationToken cancellationToken)
    {
        var payment = _paymentData.Get(request.PaymentStatusRequest.Id);

        if (payment is null)
        {
            throw new ScException("Платежная операция не найдена");
        }

        payment.Description = request.PaymentStatusRequest.Description;

        payment.State = request.PaymentStatusRequest.State;

        var response = _paymentData.UpdateStatus(payment);

        return Task.FromResult(response);
    }
}