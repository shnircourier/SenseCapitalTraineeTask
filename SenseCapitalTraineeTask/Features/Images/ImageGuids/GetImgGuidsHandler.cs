using MediatR;
using SenseCapitalTraineeTask.Data;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Images.ImageGuids;

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