using BusinessLogic.Commands;
using BusinessLogic.Queries;
using FluentValidation;
using MediatR;

namespace BusinessLogic.Validation;

public class UpdateMeetingCommandValidator : AbstractValidator<UpdateMeetingCommand>
{
    private readonly IMediator _mediator;

    public UpdateMeetingCommandValidator(IMediator mediator)
    {
        _mediator = mediator;

        RuleFor(x => x.Meeting.Title)
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(128);

        RuleFor(x => x.Meeting.Description)
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(128);

        RuleFor(x => x.Meeting.ImgId)
            .NotEmpty()
            .MustAsync(async (x, cToken ) =>
            {
                var guids = await _mediator.Send(new GetImgGuidsQuery());

                return guids.HashSet.Contains(x);
            })
            .WithMessage("Ссылка на несуществующий ключ");

        RuleFor(x => x.Meeting.RoomId)
            .NotEmpty()
            .MustAsync(async (x, cToken) =>
            {
                var guids = await _mediator.Send(new GetRoomGuidsQuery());

                return guids.HashSet.Contains(x);
            })
            .WithMessage("Ссылка на несуществующий ключ");

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