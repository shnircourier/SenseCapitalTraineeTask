using AutoMapper;
using SenseCapitalTraineeTask.Features.Meetings.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings;

/// <summary>
/// Профиль модели мероприятия
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