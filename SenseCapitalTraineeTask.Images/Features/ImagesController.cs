using MediatR;
using Microsoft.AspNetCore.Mvc;
using SenseCapitalTraineeTask.Images.Features.ImageById;
using SenseCapitalTraineeTask.Images.Features.ImageList;

namespace SenseCapitalTraineeTask.Images.Features;

[ApiController]
[Route("images")]
public class ImagesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ImagesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<List<string>> Get()
    {
        var response = await _mediator.Send(new ImageListQuery());

        return response;
    }

    [HttpGet("{id}")]
    public async Task<string> Get(string id)
    {
        var response = await _mediator.Send(new ImageByIdQuery(id));

        return response;
    }
}