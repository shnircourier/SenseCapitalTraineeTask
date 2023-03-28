using MediatR;
using SenseCapitalTraineeTask.Payment.Features.Payment.Data;

namespace SenseCapitalTraineeTask.Payment.Features.Payment.PaymentList;

public record PaymentListRequest : IRequest<List<PaymentOperation>>;