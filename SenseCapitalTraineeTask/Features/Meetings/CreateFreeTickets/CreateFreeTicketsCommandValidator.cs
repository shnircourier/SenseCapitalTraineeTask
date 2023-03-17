using FluentValidation;
using JetBrains.Annotations;
using SenseCapitalTraineeTask.Features.Meetings.CreateMeeting;

namespace SenseCapitalTraineeTask.Features.Meetings.CreateFreeTickets;

[UsedImplicitly]
public class CreateFreeTicketsCommandValidator : AbstractValidator<CreateFreeTicketsCommand>
{
    public CreateFreeTicketsCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.RequestDto.Amount)
            .NotEmpty()
            .GreaterThan(0);
    }
}