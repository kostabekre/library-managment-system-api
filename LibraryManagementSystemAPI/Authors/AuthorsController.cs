using FluentValidation;
using LibraryManagementSystemAPI.Authors.Commands;
using LibraryManagementSystemAPI.Authors.Models;
using LibraryManagementSystemAPI.Authors.Queries;
using LibraryManagementSystemAPI.Models;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemAPI.Authors;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AuthorFullInfo>>> GetAllAuthors()
    {
        var query = new GetAllAuthorsQuery();
        
        var authors = await _mediator.Send(query);
        
        return Ok(authors);
    }
    
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("{id}")]
    public async Task<ActionResult<AuthorFullInfo>> GetAuthor(int id)
    {
        var query = new GetAuthorQuery(id);
        var author = await _mediator.Send(query);
        return author == null ? NotFound() : Ok(author);
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType<Error>(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<AuthorFullInfo>> CreateAuthor(AuthorInfo info)
    {
        var command = new CreateAuthorCommand(info);
        var result = await _mediator.Send(command);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        AuthorFullInfo fullInfo = result.Data!;
        return CreatedAtAction(nameof(GetAuthor), new {fullInfo.Id}, fullInfo);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Error>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Error>(StatusCodes.Status404NotFound)]
    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> UpdateAuthor(int id, AuthorInfo info)
    {
        var command = new UpdateAuthorCommand(id, info);
        var error = await _mediator.Send(command);
        return error != null ? StatusCode(error.Code, error) : Ok();
    }
    

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteAuthor(int id)
    {
        var command = new DeleteAuthorCommand(id);
        var error = await _mediator.Send(command);
        return error != null ? NotFound() : Ok();
    }
}