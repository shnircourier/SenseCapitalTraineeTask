using MediatR;
using SenseCapitalTraineeTask.Payment.Features.Payment.Data;

namespace SenseCapitalTraineeTask.Payment.Features.Payment.UpdatePaymentStatus;

public record UpdatePaymentStatusRequest(UpdatePaymentStatusRequestDto PaymentStatusRequestDto) : IRequest<PaymentOperation>;