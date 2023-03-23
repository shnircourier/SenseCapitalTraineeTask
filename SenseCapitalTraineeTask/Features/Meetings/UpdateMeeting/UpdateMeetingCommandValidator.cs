using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using SenseCapitalTraineeTask.Features.Images.ImageById;
using SenseCapitalTraineeTask.Features.Rooms.RoomById;

namespace SenseCapitalTraineeTask.Features.Meetings.UpdateMeeting;

/// <summary>
/// Валидатор данных на обновление мероприятия
/// </summary>
[UsedImplicitly]
public class UpdateMeetingCommandValidator : AbstractValidator<UpdateMeetingCommand>
{
    /// <inheritdoc />
    public UpdateMeetingCommandValidator(IMediator mediator)
    {
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
            .NotEmpty()
            .Matches(@"^[0-9a-fA-F]{24}$")
            .WithMessage("ImgId. Некорректный формат Id. Необходимо 24 символа(0-9, a-f)")
            .MustAsync(async (x, _) =>
            {
                var response = await mediator.Send(new ImageByIdQuery(x));

                return response.Length != 0;
            })
            .WithMessage("ImgId. Ссылка на несуществующий ключ");

        RuleFor(x => x.Meeting.RoomId)
            .NotEmpty()
            .Matches(@"^[0-9a-fA-F]{24}$")
            .WithMessage("RoomId. Некорректный формат Id. Необходимо 24 символа(0-9, a-f)")
            .MustAsync(async (x, _) =>
            {
                var response = await mediator.Send(new RoomByIdQuery(x));

                return response.Length != 0;
            })
            .WithMessage("RoomId. Ссылка на несуществующий ключ");

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
