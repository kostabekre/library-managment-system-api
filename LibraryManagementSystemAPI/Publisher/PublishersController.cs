using LibraryManagementSystemAPI.Publisher.Commands;
using LibraryManagementSystemAPI.Publisher.Data;
using Microsoft.AspNetCore.Mvc;
using Mediator;

namespace LibraryManagementSystemAPI.Publisher;

[ApiController]
[Route("api/[controller]")]
public class PublishersController : ControllerBase
{
    private IMediator _mediator;

    public PublishersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PublisherFullInfo>>> GetAllPublishers()
    {
        var query = new GetAllPublishersQuery();
        IEnumerable<PublisherFullInfo> publishers = await _mediator.Send(query);
        return Ok(publishers);
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<PublisherFullInfo>> GetPublisher(int id)
    {
        var query = new GetPublisherQuery(id);
        var publisher = await _mediator.Send(query);
        if (publisher == null)
        {
            return NotFound();
        }
        return Ok(publisher);
    }

    [HttpPost]
    public async Task<ActionResult<PublisherFullInfo>> CreatePublisher(PublisherInfo info)
    {
        var command = new CreatePublisherCommand(info);
        var result = await _mediator.Send(command);
        
        if (result.IsFailure)
        {
            return StatusCode(result.Error!.Code, result.Error.Messages);
        }

        PublisherFullInfo fullInfo = result.Data!;
        
        return CreatedAtAction(nameof(GetPublisher), new {fullInfo.Id}, fullInfo);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> UpdatePublisher(int id, PublisherInfo info)
    {
        var command = new UpdatePublisherCommand(id, info);
        var error = await _mediator.Send(command);

        if (error != null)
        {
            return StatusCode(error.Code, error.Messages);
        }

        return Ok();
    }
    

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeletePublisher(int id)
    {
        var command = new DeletePublisherCommand(id);
        var error = await _mediator.Send(command);
        if (error != null)
        {
            return NotFound();
        }

        return Ok();
    }
}