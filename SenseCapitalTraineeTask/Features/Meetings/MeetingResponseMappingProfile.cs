using AutoMapper;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings;

/// <summary>
/// Профиль модели мероприятия из бд к модели ответа
/// </summary>
public class MeetingResponseMappingProfile : Profile
{
    /// <inheritdoc />
    public MeetingResponseMappingProfile()
    {
        CreateMap<Meeting, MeetingResponseDto>();
    }
}