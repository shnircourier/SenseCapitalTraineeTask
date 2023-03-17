using MediatR;

namespace SenseCapitalTraineeTask.Features.Auth.GetToken;

public record GetTokenQuery(UserRequestDto UserRequestDto) : IRequest<string>;