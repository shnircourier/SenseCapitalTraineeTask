using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using SenseCapitalTraineeTask.Features.Meetings.Data;
using SenseCapitalTraineeTask.Features.Meetings.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Auth.GetUsers;

/// <summary>
/// Логика получения списка пользователей
/// </summary>
[UsedImplicitly]
public class GetUsersHandler : IRequestHandler<GetUsersRequest, List<UserResponseDto>>
{
    private readonly IRepository<User> _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// DI
    /// </summary>
    /// <param name="repository">БД</param>
    /// <param name="mapper">Mapper</param>
    public GetUsersHandler(IRepository<User> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<List<UserResponseDto>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        var users = await _repository.Get();
        
        var response = _mapper.Map<List<UserResponseDto>>(users);
        
        return response;
    }
}