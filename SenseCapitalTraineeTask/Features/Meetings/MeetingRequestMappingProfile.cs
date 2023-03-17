using AutoMapper;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings;

/// <summary>
/// Маппер модели мероприятия
/// </summary>
public class MeetingRequestMappingProfile : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public MeetingRequestMappingProfile()
    {
        CreateMap<MeetingRequestDto, Meeting>();
    }
}