using FluentValidation;
using JetBrains.Annotations;
using MediatR;

namespace SenseCapitalTraineeTask.Features.Meetings.CreateMeeting;

/// <summary>
/// Валидатор создания мероприятия
/// </summary>
[UsedImplicitly]
public class CreateMeetingCommandValidator : AbstractValidator<CreateMeetingCommand>
{
    /// <inheritdoc />
    public CreateMeetingCommandValidator(IMediator mediator)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(x => x.Meeting.Title)
            .NotEmpty()
            .Length(10, 128);

        RuleFor(x => x.Meeting.Description)
            .NotEmpty()
            .Length(10, 256);

        RuleFor(x => x.Meeting.ImgId)
            .NotEmpty();
            // .MustAsync(async (x, _ ) =>
            // {
            //     var guids = await mediator.Send(new GetImgGuidsQuery());
            //
            //     return guids.HashSet.Contains(x);
            // })
            // .WithMessage("ImgId. Ссылка на несуществующий ключ");

            RuleFor(x => x.Meeting.RoomId)
                .NotEmpty();
            // .MustAsync(async (x, _) =>
            // {
            //     var guids = await mediator.Send(new GetRoomGuidsQuery());
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