using MediatR;

namespace SenseCapitalTraineeTask.Features.Auth.GetUsers;

public record GetUsersQuery : IRequest<List<UserResponseDto>>;