using JetBrains.Annotations;
using MediatR;

namespace SenseCapitalTraineeTask.Features.Auth.VerifyUser;

/// <summary>
/// Запрос на проверку пользователя
/// </summary>
/// <param name="UserRequestDto"></param>
[UsedImplicitly]
public record VerifyUserQuery(UserRequestDto UserRequestDto) : IRequest<bool>;