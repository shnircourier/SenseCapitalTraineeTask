using JetBrains.Annotations;
using MediatR;

namespace SenseCapitalTraineeTask.Features.Meetings.CreateTickets;

/// <summary>
/// Команда на создание билетов
/// </summary>
/// <param name="RequestDto">Тело запроса на добавление</param>
/// <param name="MeetingId">Id мероприятия</param>
[UsedImplicitly]
public record CreateTicketsCommand(CreateTicketsRequestDto RequestDto, string MeetingId) : IRequest<MeetingResponseDto>;