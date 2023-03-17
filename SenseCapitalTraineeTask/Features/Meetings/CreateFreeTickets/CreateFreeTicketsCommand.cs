using JetBrains.Annotations;
using MediatR;

namespace SenseCapitalTraineeTask.Features.Meetings.CreateFreeTickets;

/// <summary>
/// Команда на создание билетов
/// </summary>
/// <param name="RequestDto">Тело запроса на добавление</param>
[UsedImplicitly]
public record CreateFreeTicketsCommand(CreateFreeTicketsRequestDto RequestDto) : IRequest<MeetingResponseDto>;