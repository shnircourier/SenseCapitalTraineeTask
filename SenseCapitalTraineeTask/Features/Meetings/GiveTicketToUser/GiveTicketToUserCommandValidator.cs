using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using SenseCapitalTraineeTask.Features.Auth.GetUsers;
using SenseCapitalTraineeTask.Features.Meetings.UpdateMeeting;

namespace SenseCapitalTraineeTask.Features.Meetings.GiveTicketToUser;

[UsedImplicitly]
public class GiveTicketToUserCommandValidator : AbstractValidator<GiveTicketToUserCommand>
{
    private readonly IMediator _mediator;

    public GiveTicketToUserCommandValidator(IMediator mediator)
    {
        _mediator = mediator;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.RequestDto.MeetingId)
            .NotEmpty();

        RuleFor(x => x.RequestDto.UserId)
            .NotEmpty()
            .MustAsync(async (x, _) =>
            {
                var users = await _mediator.Send(new GetUsersQuery());

                return users.FirstOrDefault(u => u.Id == x) is not null;
            })
            .WithMessage("Пользователя не существует");
    }
}