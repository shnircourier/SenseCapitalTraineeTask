using AutoMapper;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings;

public class MeetingRequestMappingProfile : Profile
{
    public MeetingRequestMappingProfile()
    {
        CreateMap<MeetingRequestDto, Meeting>();
    }
}