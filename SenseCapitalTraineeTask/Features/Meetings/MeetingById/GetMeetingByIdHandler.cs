using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.Exceptions;
using SenseCapitalTraineeTask.Data;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings.MeetingById;

[UsedImplicitly]
public class GetMeetingByIdHandler : IRequestHandler<GetMeetingByIdQuery, MeetingResponseDto>
{
    private readonly IRepository<Meeting> _repository;
    private readonly IMapper _mapper;

    public GetMeetingByIdHandler(
        IRepository<Meeting> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public Task<MeetingResponseDto> Handle(GetMeetingByIdQuery request, CancellationToken cancellationToken)
    {
        var response = _mapper.Map<MeetingResponseDto>(_repository.Get(request.Id));

        if (response is null)
        {
            throw new ScException("Мероприятие не найдено");
        }
        
        return Task.FromResult(response);
    }
}