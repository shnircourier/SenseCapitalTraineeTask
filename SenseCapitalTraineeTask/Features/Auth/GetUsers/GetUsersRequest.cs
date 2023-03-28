using MediatR;

namespace SenseCapitalTraineeTask.Features.Auth.GetUsers;

/// <summary>
/// Запрос на получения списка пользователей
/// </summary>
public record GetUsersRequest : IRequest<List<UserResponseDto>>;