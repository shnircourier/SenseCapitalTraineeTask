using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using SenseCapitalTraineeTask.Features.Meetings.MeetingById;

namespace SenseCapitalTraineeTask.Features.Meetings.CreateTickets;

/// <summary>
/// Validator создания билетов
/// </summary>
[UsedImplicitly]
public class CreateTicketsValidator : AbstractValidator<CreateTicketsRequest>
{
    /// <inheritdoc />
    public CreateTicketsValidator(IMediator mediator)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.RequestDto.Amount)
            .GreaterThan(0)
            .WithMessage("Кол-во билетов не может быть отрицательным или равно 0");
        
        RuleFor(x => x.MeetingId)
            .NotEmpty()
            .WithMessage("MeetingId не может быть пустым")
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