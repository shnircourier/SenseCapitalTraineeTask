using FluentValidation;
using JetBrains.Annotations;

namespace SenseCapitalTraineeTask.Features.Meetings.CreateFreeTickets;

/// <summary>
/// Validator создания билетов
/// </summary>
[UsedImplicitly]
public class CreateFreeTicketsValidator : AbstractValidator<CreateFreeTicketsCommand>
{
    /// <inheritdoc />
    public CreateFreeTicketsValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.RequestDto.Amount)
            .NotEmpty()
            .GreaterThan(0);
    }
}