using AutoMapper;
using JetBrains.Annotations;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Auth;

/// <summary>
/// Маппер пользователя
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