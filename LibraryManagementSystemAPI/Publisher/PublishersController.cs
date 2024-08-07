using LibraryManagementSystemAPI.Models;
using LibraryManagementSystemAPI.Publisher.Commands;
using LibraryManagementSystemAPI.Publisher.Data;
using Microsoft.AspNetCore.Mvc;
using Mediator;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagementSystemAPI.Publisher;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class PublishersController : ControllerBase
{
    private readonly IMediator _mediator;

    public PublishersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PublisherFullInfo>>> GetAllPublishers()
    {
        var query = new GetAllPublishersQuery();
        IEnumerable<PublisherFullInfo> publishers = await _mediator.Send(query);
        return Ok(publishers);
    }
    
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("{id}")]
    public async Task<ActionResult<PublisherFullInfo>> GetPublisher(int id)
    {
        var query = new GetPublisherQuery(id);
        
        var publisher = await _mediator.Send(query);
        
        return publisher == null ? NotFound() : Ok(publisher);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType<Error>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PublisherFullInfo>> CreatePublisher(PublisherInfo info)
    {
        var command = new CreatePublisherCommand(info);
        
        var result = await _mediator.Send(command);
        
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        PublisherFullInfo fullInfo = result.Data!;
        
        return CreatedAtAction(nameof(GetPublisher), new {fullInfo.Id}, fullInfo);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Error>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Error>(StatusCodes.Status404NotFound)]
    [Route("{id}")]
    public async Task<ActionResult> UpdatePublisher(int id, PublisherInfo info)
    {
        var command = new UpdatePublisherCommand(id, info);
        
        var error = await _mediator.Send(command);

        return error != null ? StatusCode(error.Code, error) : Ok();
    }
    

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("{id}")]
    public async Task<ActionResult> DeletePublisher(int id)
    {
        var command = new DeletePublisherCommand(id);
        
        var error = await _mediator.Send(command);
        
        return error != null ? NotFound() : Ok();
    }
}