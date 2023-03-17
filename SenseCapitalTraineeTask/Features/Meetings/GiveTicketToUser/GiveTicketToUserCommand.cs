using JetBrains.Annotations;
using MediatR;

namespace SenseCapitalTraineeTask.Features.Meetings.GiveTicketToUser;

[UsedImplicitly]
public record GiveTicketToUserCommand(TicketRequestDto RequestDto) : IRequest<MeetingResponseDto>;