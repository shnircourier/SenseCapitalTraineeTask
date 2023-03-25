using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.ScResult;

namespace SenseCapitalTraineeTask.Features.Payments.CreatePayment;

/// <summary>
/// 
/// </summary>
/// <param name="Description"></param>
public record CreatePaymentCommand([UsedImplicitly] string? Description) : IRequest<ScResult<PaymentOperation>>;