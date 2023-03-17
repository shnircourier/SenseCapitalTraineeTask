using FluentValidation;
using SenseCapitalTraineeTask.Features.Meetings.CreateMeeting;

namespace SenseCapitalTraineeTask.Features.Meetings.CreateFreeTickets;

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