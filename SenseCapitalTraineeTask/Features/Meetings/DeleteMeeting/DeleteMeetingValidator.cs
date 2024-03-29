using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using SenseCapitalTraineeTask.Features.Meetings.MeetingById;

namespace SenseCapitalTraineeTask.Features.Meetings.DeleteMeeting;

/// <summary>
/// Валидация удаления мероприятия
/// </summary>
[UsedImplicitly]
public class DeleteMeetingValidator : AbstractValidator<DeleteMeetingRequest>
{
    /// <inheritdoc />
    public DeleteMeetingValidator(IMediator mediator)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id не может быть пустым")
            .Matches(@"^[0-9a-fA-F]{24}$")
            .WithMessage("Id. Некорректный формат Id. Необходимо 24 символа(0-9, a-f)")
            .MustAsync(async (x, _) =>
            {
                var response = await mediator.Send(new GetMeetingByIdRequest(x));

                // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
                return response is not null;
            })
            .WithMessage("Мероприятие не найдено");
    }
}