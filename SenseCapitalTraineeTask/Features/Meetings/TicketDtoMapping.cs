using AutoMapper;
using JetBrains.Annotations;
using SenseCapitalTraineeTask.Features.Meetings.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings;

/// <summary>
/// Профиль модели билета
/// </summary>
[UsedImplicitly]
public class TicketDtoMapping : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public TicketDtoMapping()
    {
        CreateMap<Ticket, TicketDto>();
    }
}