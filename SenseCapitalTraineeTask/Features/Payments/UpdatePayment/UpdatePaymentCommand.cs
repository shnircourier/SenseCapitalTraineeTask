using MediatR;
using SC.Internship.Common.ScResult;

namespace SenseCapitalTraineeTask.Features.Payments.UpdatePayment;

/// <summary>
/// 
/// </summary>
/// <param name="Request"></param>
public record UpdatePaymentCommand(UpdatePaymentRequest Request) : IRequest<ScResult<PaymentOperation>>;