using MediatR;
using SenseCapitalTraineeTask.Payment.Data;

namespace SenseCapitalTraineeTask.Payment.Features.Payment.PaymentList;

public record PaymentListQuery : IRequest<List<PaymentOperation>>;