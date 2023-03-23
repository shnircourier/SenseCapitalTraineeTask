using AutoMapper;
using JetBrains.Annotations;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings;

/// <summary>
/// Маппер модели билета
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