using JetBrains.Annotations;
using MediatR;
using SenseCapitalTraineeTask.Features.Meetings.Data;
using SenseCapitalTraineeTask.Features.Meetings.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings.UpdateMeetingsImageId;

/// <summary>
/// Обновление мероприятий содержащих определенную картинку
/// </summary>
[UsedImplicitly]
public class UpdateMeetingsImageIdHandler : IRequestHandler<UpdateMeetingsImageIdRequest>
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
    public async Task Handle(UpdateMeetingsImageIdRequest request, CancellationToken cancellationToken)
    {
        await _repository.UpdateManyImageId(request.ImageId, null);
    }
}