using JetBrains.Annotations;
using MediatR;

namespace SenseCapitalTraineeTask.Features.Auth.VerifyUser;

[UsedImplicitly]
public record VerifyUserQuery(UserRequestDto UserRequestDto) : IRequest<bool>;