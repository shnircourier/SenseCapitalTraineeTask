using MediatR;

namespace SenseCapitalTraineeTask.Features.Meetings.UpdateMeetingsImageId;

/// <summary>
/// Команда множественного обновления
/// </summary>
/// <param name="ImageId"></param>
public record UpdateMeetingsImageIdRequest(string ImageId) : IRequest;