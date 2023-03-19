using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using SenseCapitalTraineeTask.Features.Images.ImageGuids;
using SenseCapitalTraineeTask.Features.Rooms.RoomGuids;

namespace SenseCapitalTraineeTask.Features.Meetings.CreateMeeting;

/// <summary>
/// Валидатор создания мероприятия
/// </summary>
[UsedImplicitly]
public class CreateMeetingCommandValidator : AbstractValidator<CreateMeetingCommand>
{
    private readonly IMediator _mediator;

    /// <inheritdoc />
    public CreateMeetingCommandValidator(IMediator mediator)
    {
        _mediator = mediator;

        RuleLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(x => x.Meeting.Title)
            .NotEmpty()
            .Length(10, 128);

        RuleFor(x => x.Meeting.Description)
            .NotEmpty()
            .Length(10, 256);

        RuleFor(x => x.Meeting.ImgId)
            .NotEmpty();
            // .MustAsync(async (x, cToken ) =>
            // {
            //     var guids = await _mediator.Send(new GetImgGuidsQuery());
            //
            //     return guids.HashSet.Contains(x);
            // })
            // .WithMessage("ImgId. Ссылка на несуществующий ключ");

        RuleFor(x => x.Meeting.RoomId)
            .NotEmpty();
            // .MustAsync(async (x, cToken) =>
            // {
            //     var guids = await _mediator.Send(new GetRoomGuidsQuery());
            //
            //     return guids.HashSet.Contains(x);
            // })
            // .WithMessage("RoomId. Ссылка на несуществующий ключ");

        RuleFor(x => x.Meeting.BeginAt)
            .NotEmpty()
            .Must((x,d ) => d < x.Meeting.EndAt)
            .WithMessage("Дата начала не может быть позже даты окончания");

        RuleFor(x => x.Meeting.EndAt)
            .NotEmpty()
            .Must((x, d) => d > x.Meeting.BeginAt)
            .WithMessage("Дата окончания не может быть раньше даты начала");
    }
}