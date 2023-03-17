using FluentValidation;
using JetBrains.Annotations;
using SenseCapitalTraineeTask.Features.Meetings.CreateMeeting;

namespace SenseCapitalTraineeTask.Features.Meetings.CreateFreeTickets;

/// <summary>
/// Валидатор создания билетов
/// </summary>
[UsedImplicitly]
public class CreateFreeTicketsCommandValidator : AbstractValidator<CreateFreeTicketsCommand>
{
    /// <inheritdoc />
    public CreateFreeTicketsCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.RequestDto.Amount)
            .NotEmpty()
            .GreaterThan(0);
    }
}