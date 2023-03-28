using MediatR;
using SC.Internship.Common.ScResult;

namespace SenseCapitalTraineeTask.Features.Payments.UpdatePayment;

/// <summary>
/// 
/// </summary>
/// <param name="RequestDto"></param>
public record UpdatePaymentRequest(UpdatePaymentRequestDto RequestDto) : IRequest<ScResult<PaymentOperation>>;