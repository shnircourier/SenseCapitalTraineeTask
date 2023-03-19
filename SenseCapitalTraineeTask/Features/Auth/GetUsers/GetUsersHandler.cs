using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using SenseCapitalTraineeTask.Data;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Auth.GetUsers;

/// <summary>
/// Логика получения списка пользователей
/// </summary>
[UsedImplicitly]
public class GetUsersHandler : IRequestHandler<GetUsersQuery, List<UserResponseDto>>
{
    private readonly IRepository<Meeting> _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// DI
    /// </summary>
    /// <param name="repository">БД</param>
    /// <param name="mapper">Автомапер</param>
    public GetUsersHandler(IRepository<Meeting> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public Task<List<UserResponseDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        // var users = _repository.GetUser();
        //
        // var response = _mapper.Map<List<UserResponseDto>>(users);
        //
        // return Task.FromResult(response);
        throw new NotImplementedException();
    }
}