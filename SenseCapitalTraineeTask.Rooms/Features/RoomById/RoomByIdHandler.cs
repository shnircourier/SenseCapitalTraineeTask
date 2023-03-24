using MediatR;
using SC.Internship.Common.Exceptions;
using SenseCapitalTraineeTask.Rooms.Data;
using SenseCapitalTraineeTask.Rooms.Data.Entities;

namespace SenseCapitalTraineeTask.Rooms.Features.RoomById;

public class RoomByIdHandler : IRequestHandler<RoomByIdQuery, string>
{
    private readonly IRepository<Room> _repository;

    public RoomByIdHandler(IRepository<Room> repository)
    {
        _repository = repository;
    }
    
    public async Task<string> Handle(RoomByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.Get(request.Id);

        if (result is null)
        {
            throw new ScException("Помещение не найдена");
        }
        
        return result.Id;
    }
}