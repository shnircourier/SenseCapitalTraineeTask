using MediatR;
using SenseCapitalTraineeTask.Payment.Data;

namespace SenseCapitalTraineeTask.Payment.Features.Payment.CreatePayment;

public record CreatePaymentCommand(string? Description) : IRequest<PaymentOperation>;