using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using SenseCapitalTraineeTask.Features.Auth.GetUsers;
using SenseCapitalTraineeTask.Features.Meetings.MeetingById;

namespace SenseCapitalTraineeTask.Features.Meetings.CheckUserTicket;

/// <summary>
/// Validator данных на выдачу билета пользователю
/// </summary>
[UsedImplicitly]
public class CheckUserTicketValidator : AbstractValidator<CheckUserTicketRequest>
{
    /// <inheritdoc />
    public CheckUserTicketValidator(IMediator mediator)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(x => x.RequestDto.UserId)
            .NotEmpty()
            .WithMessage("UserId. Поле обязательно к заполнению")
            .Matches(@"^[0-9a-fA-F]{24}$")
            .WithMessage("UserId. Некорректный формат Id. Необходимо 24 символа(0-9, a-f)")
            .MustAsync(async (x, _) =>
            {
                var users = await mediator.Send(new GetUsersRequest());
            
                return users.FirstOrDefault(u => u.Id == x) is not null;
            })
            .WithMessage("Пользователя не существует");
        
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