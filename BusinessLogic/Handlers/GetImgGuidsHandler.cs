using BusinessLogic.Models;
using BusinessLogic.Queries;
using Data;
using Data.Entities;
using MediatR;

namespace BusinessLogic.Handlers;

public class GetImgGuidsHandler : IRequestHandler<GetImgGuidsQuery, ImgGuidsResponse>
{
    private readonly IRepository<Meeting> _repository;

    public GetImgGuidsHandler(IRepository<Meeting> repository)
    {
        _repository = repository;
    }
    
    public Task<ImgGuidsResponse> Handle(GetImgGuidsQuery request, CancellationToken cancellationToken)
    {
        var response = _repository.GetAvailableImgGuids();
        
        return Task.FromResult(new ImgGuidsResponse(response));
    }
}