using FluentValidation;
using LibraryManagementSystemAPI.Genre.Commands;
using LibraryManagementSystemAPI.Genre.Data;
using LibraryManagementSystemAPI.Genre.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemAPI.Genre;

[ApiController]
[Route("api/[controller]")]
public class GenresController : ControllerBase
{
    private IMediator _mediator;

    public GenresController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<GenreFullInfo>> GetGenre(int id)
    {
        var query = new GetGenreQuery(id);
        var genre = await _mediator.Send(query);
        if (genre == null)
        {
            return NotFound();
        }
        return Ok(genre);
    }
    
    [HttpDelete]
    public async Task<ActionResult> RemoveGenre(int id)
    {
        var command = new RemoveGenreCommand(id);
        var error = await _mediator.Send(command);
        if (error != null)
        {
            return NotFound();
        }
        
        return Ok();
    }
    
    [HttpGet]
    [Route("get_all")]
    public async Task<ActionResult<IEnumerable<GenreFullInfo>>> GetAllGenres()
    {
        var query = new GetAllGenresQuery();
        var genres = await _mediator.Send(query);
        return Ok(genres);
    }
    
    [HttpPost]
    public async Task<ActionResult<GenreFullInfo>> CreateGenre(GenreInfo info)
    {
        var command = new CreateGenreCommand(info);
        var result = await _mediator.Send(command);
        if (result.IsFailure)
        {
            return StatusCode(result.Error!.Code, result.Error.Messages);
        }
        return CreatedAtAction(nameof(GetGenre), new {result.Data!.Id}, result.Data);
    }
}