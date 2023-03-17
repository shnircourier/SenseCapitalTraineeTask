using JetBrains.Annotations;
using MediatR;

namespace SenseCapitalTraineeTask.Features.Meetings.CreateFreeTickets;

[UsedImplicitly]
public record CreateFreeTicketsCommand(CreateFreeTicketsRequestDto RequestDto) : IRequest<MeetingResponseDto>;