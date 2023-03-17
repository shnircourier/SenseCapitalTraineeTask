using MediatR;

namespace SenseCapitalTraineeTask.Features.Auth.GetToken;

public record GetTokenQuery : IRequest<string>;