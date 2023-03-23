using MediatR;
using SenseCapitalTraineeTask.Rooms.Data;
using SenseCapitalTraineeTask.Rooms.Data.Entities;

namespace SenseCapitalTraineeTask.Rooms.Features.RoomById;

public class RoomByIdHandler : IRequestHandler<RoomByIdQuery, string?>
{
    private readonly IRepository<Room> _repository;

    public RoomByIdHandler(IRepository<Room> repository)
    {
        _repository = repository;
    }
    
    public async Task<string?> Handle(RoomByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.Get(request.Id);

        // ReSharper disable once ConstantConditionalAccessQualifier
        return request?.Id;
    }
}