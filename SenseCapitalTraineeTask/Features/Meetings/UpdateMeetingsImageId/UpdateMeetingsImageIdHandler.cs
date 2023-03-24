using MediatR;
using SenseCapitalTraineeTask.Data;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings.UpdateMeetingsImageId;

public class UpdateMeetingsImageIdHandler : IRequestHandler<UpdateMeetingsImageIdCommand>
{
    private readonly IRepository<Meeting> _repository;

    public UpdateMeetingsImageIdHandler(IRepository<Meeting> repository)
    {
        _repository = repository;
    }
    
    public async Task Handle(UpdateMeetingsImageIdCommand request, CancellationToken cancellationToken)
    {
        await _repository.UpdateManyImageId(request.ImageId, null);
    }
}