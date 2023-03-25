using MediatR;
using SenseCapitalTraineeTask.Payment.Data;

namespace SenseCapitalTraineeTask.Payment.Features.Payment.UpdatePaymentStatus;

public record UpdatePaymentStatusCommand(UpdatePaymentStatusRequest PaymentStatusRequest) : IRequest<PaymentOperation>;