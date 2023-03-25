using MediatR;
using SC.Internship.Common.ScResult;

namespace SenseCapitalTraineeTask.Features.Payments.CreatePayment;

public record CreatePaymentCommand(string? Description) : IRequest<ScResult<PaymentOperation>>;