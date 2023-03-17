using AutoMapper;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings;

public class TicketDtoMapping : Profile
{
    public TicketDtoMapping()
    {
        CreateMap<Ticket, TicketDto>();
    }
}