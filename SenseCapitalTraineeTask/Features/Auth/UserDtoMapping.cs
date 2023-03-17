using AutoMapper;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Auth;

public class UserDtoMapping : Profile
{
    public UserDtoMapping()
    {
        CreateMap<User, UserResponseDto>();
    }
}