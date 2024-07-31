using FluentValidation;
using LibraryManagementSystemAPI.Authors.Commands;
using LibraryManagementSystemAPI.Authors.Models;
using LibraryManagementSystemAPI.Authors.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemAPI.Authors;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorFullInfo>>> GetAllAuthors()
    {
        var query = new GetAllAuthorsQuery();
        var authors = await _mediator.Send(query);
        return Ok(authors);
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<AuthorFullInfo>> GetAuthor(int id)
    {
        var query = new GetAuthorQuery(id);
        var author = await _mediator.Send(query);
        if (author == null)
        {
            return NotFound();
        }
        return Ok(author);
    }

    [HttpPost]
    public async Task<ActionResult<AuthorFullInfo>> CreateAuthor(AuthorInfo info)
    {
        var command = new CreateAuthorCommand(info);
        var result = await _mediator.Send(command);
        if (result.IsFailure)
        {
            return StatusCode(result.Error!.Code, result.Error.Messages);
        }
        AuthorFullInfo fullInfo = result.Data!;
        return CreatedAtAction(nameof(GetAuthor), new {fullInfo.Id}, fullInfo);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> UpdateAuthor(int id, AuthorInfo info)
    {
        var command = new UpdateAuthorCommand(id, info);
        var error = await _mediator.Send(command);
        if (error != null)
        {
            return StatusCode(error.Code, error.Messages);
        }

        return Ok();
    }
    

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteAuthor(int id)
    {
        var command = new DeleteAuthorCommand(id);
        var error = await _mediator.Send(command);
        if (error != null)
        {
            return NotFound();
        }

        return Ok();
    }
}