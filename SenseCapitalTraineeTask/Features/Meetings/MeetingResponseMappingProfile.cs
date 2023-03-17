using AutoMapper;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings;

public class MeetingResponseMappingProfile : Profile
{
    public MeetingResponseMappingProfile()
    {
        CreateMap<Meeting, MeetingResponseDto>();
    }
}