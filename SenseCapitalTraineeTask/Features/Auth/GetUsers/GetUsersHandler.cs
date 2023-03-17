using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using SenseCapitalTraineeTask.Data;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Auth.GetUsers;

[UsedImplicitly]
public class GetUsersHandler : IRequestHandler<GetUsersQuery, List<UserResponseDto>>
{
    private readonly IRepository<Meeting> _repository;
    private readonly IMapper _mapper;

    public GetUsersHandler(IRepository<Meeting> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public Task<List<UserResponseDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = _repository.GetUser();

        var response = _mapper.Map<List<UserResponseDto>>(users);

        return Task.FromResult(response);
    }
}