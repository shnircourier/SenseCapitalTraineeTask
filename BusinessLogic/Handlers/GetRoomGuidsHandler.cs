using BusinessLogic.Models;
using BusinessLogic.Queries;
using Data;
using Data.Entities;
using MediatR;

namespace BusinessLogic.Handlers;

public class GetRoomGuidsHandler : IRequestHandler<GetRoomGuidsQuery, RoomGuidsResponse>
{
    private readonly IRepository<Meeting> _repository;

    public GetRoomGuidsHandler(IRepository<Meeting> repository)
    {
        _repository = repository;
    }
    
    public Task<RoomGuidsResponse> Handle(GetRoomGuidsQuery request, CancellationToken cancellationToken)
    {
        var response = _repository.GetAvailableRoomGuids();
        
        return Task.FromResult(new RoomGuidsResponse(response));
    }
}