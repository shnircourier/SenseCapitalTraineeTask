using MediatR;
using SC.Internship.Common.ScResult;

namespace SenseCapitalTraineeTask.Features.Payments.UpdatePayment;

public record UpdatePaymentCommand(UpdatePaymentRequest Request) : IRequest<ScResult<PaymentOperation>>;