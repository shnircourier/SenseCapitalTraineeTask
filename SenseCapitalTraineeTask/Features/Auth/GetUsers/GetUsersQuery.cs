using MediatR;

namespace SenseCapitalTraineeTask.Features.Auth.GetUsers;

/// <summary>
/// Запрос на получения списка пользователей
/// </summary>
public record GetUsersQuery : IRequest<List<UserResponseDto>>;