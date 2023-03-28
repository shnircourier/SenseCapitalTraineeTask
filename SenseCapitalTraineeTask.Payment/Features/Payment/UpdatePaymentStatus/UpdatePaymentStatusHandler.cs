using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.Exceptions;
using SenseCapitalTraineeTask.Payment.Features.Payment.Data;

namespace SenseCapitalTraineeTask.Payment.Features.Payment.UpdatePaymentStatus;

[UsedImplicitly]
public class UpdatePaymentStatusHandler : IRequestHandler<UpdatePaymentStatusRequest, PaymentOperation>
{
    private readonly PaymentData _paymentData;

    public UpdatePaymentStatusHandler(PaymentData paymentData)
    {
        _paymentData = paymentData;
    }
    
    public Task<PaymentOperation> Handle(UpdatePaymentStatusRequest request, CancellationToken cancellationToken)
    {
        var payment = _paymentData.Get(request.PaymentStatusRequestDto.Id);

        if (payment is null)
        {
            throw new ScException("Платежная операция не найдена");
        }

        payment.Description = request.PaymentStatusRequestDto.Description;

        payment.State = request.PaymentStatusRequestDto.State;

        var response = _paymentData.UpdateStatus(payment);

        return Task.FromResult(response);
    }
}