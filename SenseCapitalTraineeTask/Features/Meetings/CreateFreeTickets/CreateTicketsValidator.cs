using FluentValidation;
using JetBrains.Annotations;

namespace SenseCapitalTraineeTask.Features.Meetings.CreateFreeTickets;

/// <summary>
/// Validator создания билетов
/// </summary>
[UsedImplicitly]
public class CreateTicketsValidator : AbstractValidator<CreateTicketsCommand>
{
    /// <inheritdoc />
    public CreateTicketsValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.RequestDto.Amount)
            .NotEmpty()
            .WithMessage("Поле обязательно к заполнению")
            .GreaterThan(0)
            .WithMessage("Кол-во билетов не может быть отрицательным или равно 0");
    }
}