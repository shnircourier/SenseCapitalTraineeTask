using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using SenseCapitalTraineeTask.Features.Auth.GetUsers;

namespace SenseCapitalTraineeTask.Features.Meetings.GiveTicketToUser;

/// <summary>
/// Validator данных на выдачу билета пользователю
/// </summary>
[UsedImplicitly]
public class GiveTicketToUserValidator : AbstractValidator<GiveTicketToUserCommand>
{
    /// <inheritdoc />
    public GiveTicketToUserValidator(IMediator mediator)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(x => x.RequestDto.UserId)
            .NotEmpty()
            .WithMessage("UserId. Поле обязательно к заполнению")
            .Matches(@"^[0-9a-fA-F]{24}$")
            .WithMessage("UserId. Некорректный формат Id. Необходимо 24 символа(0-9, a-f)")
            .MustAsync(async (x, _) =>
            {
                var users = await mediator.Send(new GetUsersQuery());
            
                return users.FirstOrDefault(u => u.Id == x) is not null;
            })
            .WithMessage("Пользователя не существует");
    }
}