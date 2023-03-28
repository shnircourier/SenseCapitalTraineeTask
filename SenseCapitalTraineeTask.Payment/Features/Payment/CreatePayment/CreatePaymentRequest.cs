using MediatR;
using SenseCapitalTraineeTask.Payment.Features.Payment.Data;

namespace SenseCapitalTraineeTask.Payment.Features.Payment.CreatePayment;

public record CreatePaymentRequest(string? Description) : IRequest<PaymentOperation>;