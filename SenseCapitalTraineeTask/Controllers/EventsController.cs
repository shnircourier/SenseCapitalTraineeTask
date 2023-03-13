using Microsoft.AspNetCore.Mvc;

namespace SenseCapitalTraineeTask.Controllers;

[ApiController]
[Route("events")]
public class EventsController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id:guid}")]
    public ActionResult Get(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public ActionResult Create()
    {
        throw new NotImplementedException();
    }

    [HttpPut("{id:guid}")]
    public ActionResult Update(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id:guid}")]
    public ActionResult Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}