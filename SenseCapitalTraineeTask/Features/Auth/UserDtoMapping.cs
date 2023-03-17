using AutoMapper;
using JetBrains.Annotations;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Auth;

[UsedImplicitly]
public class UserDtoMapping : Profile
{
    public UserDtoMapping()
    {
        CreateMap<User, UserResponseDto>();
    }
}