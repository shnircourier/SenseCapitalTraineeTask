using AutoMapper;
using BusinessLogic.Models;
using Data.Entities;

namespace BusinessLogic.Profiles;

public class MeetingRequestProfile : Profile
{
    public MeetingRequestProfile()
    {
        CreateMap<MeetingRequest, Meeting>();
    }
}