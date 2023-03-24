using MediatR;
using SenseCapitalTraineeTask.Data;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings.DeleteManyMeetingsByRoomId;

public class DeleteManyMeetingsByRoomIdHandler : IRequestHandler<DeleteManyMeetingsByRoomIdCommand>
{
    private readonly IRepository<Meeting> _repository;

    public DeleteManyMeetingsByRoomIdHandler(IRepository<Meeting> repository)
    {
        _repository = repository;
    }
    
    public async Task Handle(DeleteManyMeetingsByRoomIdCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteManyMeetingByRoomId(request.RoomId);
    }
}