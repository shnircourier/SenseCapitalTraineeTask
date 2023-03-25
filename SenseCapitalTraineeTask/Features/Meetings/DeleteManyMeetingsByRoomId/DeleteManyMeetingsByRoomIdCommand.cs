using MediatR;

namespace SenseCapitalTraineeTask.Features.Meetings.DeleteManyMeetingsByRoomId;

/// <summary>
/// Команда удаления мероприятий по id помещений
/// </summary>
/// <param name="RoomId"></param>
public record DeleteManyMeetingsByRoomIdCommand(string RoomId) : IRequest;