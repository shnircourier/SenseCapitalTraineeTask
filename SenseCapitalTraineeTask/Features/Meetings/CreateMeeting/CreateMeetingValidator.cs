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
            .Length(10, 128);

        RuleFor(x => x.Meeting.Description)
            .NotEmpty()
            .Length(10, 256);

        RuleFor(x => x.Meeting.ImgId)
            .NotEmpty()
            .Matches(@"^[0-9a-fA-F]{24}$")
            .WithMessage("ImgId. Некорректный формат Id. Необходимо 24 символа(0-9, a-f)")
            .MustAsync(async (x, _) =>
            {
                var response = await mediator.Send(new ImageByIdRequest(x));

                return response.Result is not null;
            })
            .WithMessage("ImgId. Ссылка на несуществующий ключ");

        RuleFor(x => x.Meeting.RoomId)
            .NotEmpty()
            .Matches(@"^[0-9a-fA-F]{24}$")
            .WithMessage("RoomId. Некорректный формат Id. Необходимо 24 символа(0-9, a-f)")
            .MustAsync(async (x, _) =>
            {
                var response = await mediator.Send(new RoomByIdRequest(x));

                return response.Result is not null;
            })
            .WithMessage("RoomId. Ссылка на несуществующий ключ");

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