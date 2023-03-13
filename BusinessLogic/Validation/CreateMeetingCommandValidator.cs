using BusinessLogic.Commands;
using Data;
using Data.Entities;
using FluentValidation;

namespace BusinessLogic.Validation;

public class CreateMeetingCommandValidator : AbstractValidator<CreateMeetingCommand>
{
    private readonly IRepository<Meeting> _repository;

    public CreateMeetingCommandValidator(IRepository<Meeting> repository)
    {
        _repository = repository;

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
            .Must(x => _repository.GetAvailableImgGuids().Contains(x))
            .WithMessage("Ссылка на несуществующий ключ");

        RuleFor(x => x.Meeting.RoomId)
            .NotEmpty()
            .Must(x => _repository.GetAvailableRoomGuids().Contains(x))
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