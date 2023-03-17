using JetBrains.Annotations;
using MediatR;
using SenseCapitalTraineeTask.Features.Auth.GetUsers;

namespace SenseCapitalTraineeTask.Features.Auth.VerifyUser;

[UsedImplicitly]
public class VerifyUserHandler : IRequestHandler<VerifyUserQuery, bool>
{
    private readonly IMediator _mediator;

    public VerifyUserHandler(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task<bool> Handle(VerifyUserQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetUsersQuery(), cancellationToken);

        return response.FirstOrDefault(r =>
            r.Username == request.UserRequestDto.Username
            && r.Password == request.UserRequestDto.Password)
            is not null;
    }
}