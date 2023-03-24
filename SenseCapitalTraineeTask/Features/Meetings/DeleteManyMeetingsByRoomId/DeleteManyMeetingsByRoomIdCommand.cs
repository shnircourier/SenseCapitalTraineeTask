using MediatR;

namespace SenseCapitalTraineeTask.Features.Meetings.DeleteManyMeetingsByRoomId;

public record DeleteManyMeetingsByRoomIdCommand(string RoomId) : IRequest;