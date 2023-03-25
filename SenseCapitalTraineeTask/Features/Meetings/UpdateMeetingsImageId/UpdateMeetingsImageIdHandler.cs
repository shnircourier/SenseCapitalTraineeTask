using JetBrains.Annotations;
using MediatR;
using SenseCapitalTraineeTask.Data;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings.UpdateMeetingsImageId;

/// <summary>
/// Обновление мероприятий содержащих определенную картинку
/// </summary>
[UsedImplicitly]
public class UpdateMeetingsImageIdHandler : IRequestHandler<UpdateMeetingsImageIdCommand>
{
    private readonly IRepository<Meeting> _repository;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repository"></param>
    public UpdateMeetingsImageIdHandler(IRepository<Meeting> repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task Handle(UpdateMeetingsImageIdCommand request, CancellationToken cancellationToken)
    {
        await _repository.UpdateManyImageId(request.ImageId, null);
    }
}