using JetBrains.Annotations;
using MediatR;

namespace SenseCapitalTraineeTask.Features.Meetings.GiveTicketToUser;

/// <summary>
/// Команда выдачи билета пользователю
/// </summary>
/// <param name="RequestDto"></param>
/// <param name="MeetingId"></param>
[UsedImplicitly]
public record GiveTicketToUserRequest(TicketRequestDto RequestDto, string MeetingId) : IRequest<MeetingResponseDto>;