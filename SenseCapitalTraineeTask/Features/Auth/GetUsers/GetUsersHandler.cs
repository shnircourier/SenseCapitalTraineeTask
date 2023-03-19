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
    private readonly IRepository<User> _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// DI
    /// </summary>
    /// <param name="repository">БД</param>
    /// <param name="mapper">Автомапер</param>
    public GetUsersHandler(IRepository<User> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<List<UserResponseDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _repository.Get();
        
        var response = _mapper.Map<List<UserResponseDto>>(users);
        
        return response;
    }
}