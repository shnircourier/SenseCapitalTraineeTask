using AutoMapper;
using JetBrains.Annotations;
using SenseCapitalTraineeTask.Features.Meetings.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Auth;

/// <summary>
/// Профиль пользователя
/// </summary>
[UsedImplicitly]
public class UserDtoMapping : Profile
{
    /// <inheritdoc />
    public UserDtoMapping()
    {
        CreateMap<User, UserResponseDto>();
    }
}