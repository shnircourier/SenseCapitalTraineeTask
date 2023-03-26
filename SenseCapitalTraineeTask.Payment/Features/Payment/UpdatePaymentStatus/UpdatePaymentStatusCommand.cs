using MediatR;
using SenseCapitalTraineeTask.Payment.Features.Payment.Data;

namespace SenseCapitalTraineeTask.Payment.Features.Payment.UpdatePaymentStatus;

public record UpdatePaymentStatusCommand(UpdatePaymentStatusRequest PaymentStatusRequest) : IRequest<PaymentOperation>;