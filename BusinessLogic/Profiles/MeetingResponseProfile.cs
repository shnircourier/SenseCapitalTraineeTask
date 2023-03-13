using AutoMapper;
using BusinessLogic.Models;
using Data.Entities;

namespace BusinessLogic.Profiles;

public class MeetingResponseProfile : Profile
{
    public MeetingResponseProfile()
    {
        CreateMap<Meeting, MeetingResponse>();
    }
}