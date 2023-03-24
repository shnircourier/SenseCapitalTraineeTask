using MediatR;

namespace SenseCapitalTraineeTask.Features.Meetings.UpdateMeetingsImageId;

public record UpdateMeetingsImageIdCommand(string ImageId) : IRequest;