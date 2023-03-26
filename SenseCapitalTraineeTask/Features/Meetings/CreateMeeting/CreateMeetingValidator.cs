using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using SenseCapitalTraineeTask.Features.Images.ImageById;
using SenseCapitalTraineeTask.Features.Rooms.RoomById;

namespace SenseCapitalTraineeTask.Features.Meetings.CreateMeeting;

/// <summary>
/// Validator создания мероприятия
/// </summary>
[UsedImplicitly]
public class CreateMeetingValidator : AbstractValidator<CreateMeetingCommand>
{
    /// <inheritdoc />
    public CreateMeetingValidator(IMediator mediator)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Meeting.TicketPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Цена билета не может быть отрицательной");
        
        RuleFor(x => x.Meeting.Title)
            .NotEmpty()
            .WithMessage("Title. Поле обязательно к заполнению")
            .Length(10, 128)
            .WithMessage("Title. Кол-во символов должно быть от 10 до 128");

        RuleFor(x => x.Meeting.Description)
            .NotEmpty()
            .WithMessage("Description. Поле обязательно к заполнению")
            .Length(10, 256)
            .WithMessage("Description. Кол-во символов должно быть от 10 до 128");

        RuleFor(x => x.Meeting.ImgId)
            .NotEmpty()
            .WithMessage("ImgId. Поле обязательно к заполнению")
            .Matches(@"^[0-9a-fA-F]{24}$")
            .WithMessage("ImgId. Некорректный формат Id. Необходимо 24 символа(0-9, a-f)")
            .MustAsync(async (x, _) =>
            {
                var response = await mediator.Send(new ImageByIdRequest(x));

                return response.Result is not null;
            })
            .WithMessage("ImgId. Выбранной картинки не существует");

        RuleFor(x => x.Meeting.RoomId)
            .NotEmpty()
            .WithMessage("RoomId. Поле обязательно к заполнению")
            .Matches(@"^[0-9a-fA-F]{24}$")
            .WithMessage("RoomId. Некорректный формат Id. Необходимо 24 символа(0-9, a-f)")
            .MustAsync(async (x, _) =>
            {
                var response = await mediator.Send(new RoomByIdRequest(x));

                return response.Result is not null;
            })
            .WithMessage("RoomId. Выбранного помещения не существует");

        RuleFor(x => x.Meeting.BeginAt)
            .NotEmpty()
            .WithMessage("BeginAt. Поле обязательно к заполнению")
            .Must((x, d) => d == x.Meeting.EndAt)
            .WithMessage("BeginAt. Дата начала равна дате окончания")
            .Must((x,d ) => d < x.Meeting.EndAt)
            .WithMessage("BeginAt. Дата начала не может быть позже даты окончания");

        RuleFor(x => x.Meeting.EndAt)
            .NotEmpty()
            .WithMessage("EndAt. Поле обязательно к заполнению")
            .Must((x, d) => d == x.Meeting.BeginAt)
            .WithMessage("EndAt. Дата начала равна дате окончания")
            .Must((x, d) => d > x.Meeting.BeginAt)
            .WithMessage("EndAt. Дата окончания не может быть раньше даты начала");
    }
}