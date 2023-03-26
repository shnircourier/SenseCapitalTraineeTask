using FluentValidation;
using JetBrains.Annotations;
using MediatR;

namespace SenseCapitalTraineeTask.Features.Meetings.MeetingById;

/// <summary>
/// Обработчик id мероприятия
/// </summary>
[UsedImplicitly]
public class GetMeetingByIdValidator : AbstractValidator<GetMeetingByIdRequest>
{
    /// <inheritdoc />
    public GetMeetingByIdValidator(IMediator mediator)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id не может быть пустым")
            .Matches(@"^[0-9a-fA-F]{24}$")
            .WithMessage("Id. Некорректный формат Id. Необходимо 24 символа(0-9, a-f)");
    }
}