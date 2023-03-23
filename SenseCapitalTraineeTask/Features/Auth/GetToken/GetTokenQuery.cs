using MediatR;

namespace SenseCapitalTraineeTask.Features.Auth.GetToken;

/// <summary>
/// Запрос на получение токена
/// </summary>
/// <param name="UserRequestDto"></param>
public record GetTokenQuery(UserRequestDto UserRequestDto) : IRequest<string>;