using AutoMapper;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings;

/// <summary>
/// Маппер модели мероприятия из бд к модели ответа
/// </summary>
public class MeetingResponseMappingProfile : Profile
{
    public MeetingResponseMappingProfile()
    {
        CreateMap<Meeting, MeetingResponseDto>();
    }
}