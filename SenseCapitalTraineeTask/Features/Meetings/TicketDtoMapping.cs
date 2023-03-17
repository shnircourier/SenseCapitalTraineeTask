using AutoMapper;
using JetBrains.Annotations;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings;

[UsedImplicitly]
public class TicketDtoMapping : Profile
{
    public TicketDtoMapping()
    {
        CreateMap<Ticket, TicketDto>();
    }
}