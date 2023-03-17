using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using SenseCapitalTraineeTask.Features.Images.ImageGuids;
using SenseCapitalTraineeTask.Features.Rooms.RoomGuids;

namespace SenseCapitalTraineeTask.Features.Meetings.UpdateMeeting;

/// <summary>
/// Валидатор данных на обновление мероприятия
/// </summary>
[UsedImplicitly]
public class UpdateMeetingCommandValidator : AbstractValidator<UpdateMeetingCommand>
{
    private readonly IMediator _mediator;

    /// <inheritdoc />
    public UpdateMeetingCommandValidator(IMediator mediator)
    {
        _mediator = mediator;

        RuleLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(x => x.Meeting.Title)
            .NotEmpty()
            .WithMessage("Поле обязательно к заполнению")
            .Length(10, 128);

        RuleFor(x => x.Meeting.Description)
            .NotEmpty()
            .WithMessage("Поле обязательно к заполнению")
            .Length(10, 128);

        RuleFor(x => x.Meeting.ImgId)
            .NotEmpty()
            .WithMessage("Поле обязательно к заполнению")
            .MustAsync(async (x, cToken ) =>
            {
                var guids = await _mediator.Send(new GetImgGuidsQuery());

                return guids.HashSet.Contains(x);
            })
            .WithMessage("Ссылка на несуществующий ключ");

        RuleFor(x => x.Meeting.RoomId)
            .NotEmpty()
            .WithMessage("Поле обязательно к заполнению")
            .MustAsync(async (x, cToken) =>
            {
                var guids = await _mediator.Send(new GetRoomGuidsQuery());

                return guids.HashSet.Contains(x);
            })
            .WithMessage("Ссылка на несуществующий ключ");

        RuleFor(x => x.Meeting.BeginAt)
            .NotEmpty()
            .WithMessage("Поле обязательно к заполнению")
            .Must((x,d ) => d < x.Meeting.EndAt)
            .WithMessage("Дата начала не может быть позже даты окончания");

        RuleFor(x => x.Meeting.EndAt)
            .NotEmpty()
            .WithMessage("Поле обязательно к заполнению")
            .Must((x, d) => d > x.Meeting.BeginAt)
            .WithMessage("Дата окончания не может быть раньше даты начала");
    }
}
