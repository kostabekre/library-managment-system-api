using FluentValidation;
using LibraryManagementSystemAPI.Genre.Commands;
using LibraryManagementSystemAPI.Genre.Data;
using LibraryManagementSystemAPI.Genre.Queries;
using LibraryManagementSystemAPI.Models;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemAPI.Genre;

[ApiController]
[Route("api/[controller]")]
public class GenresController : ControllerBase
{
    private readonly IMediator _mediator;

    public GenresController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("{id}")]
    public async Task<ActionResult<GenreFullInfo>> GetGenre(int id)
    {
        var query = new GetGenreQuery(id);
        
        var genre = await _mediator.Send(query);
        
        return genre == null ? NotFound() : Ok(genre);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete]
    public async Task<ActionResult> RemoveGenre(int id)
    {
        var command = new RemoveGenreCommand(id);
        
        var error = await _mediator.Send(command);
        
        return error != null ? NotFound() : Ok();
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    [Route("get_all")]
    public async Task<ActionResult<IEnumerable<GenreFullInfo>>> GetAllGenres()
    {
        var query = new GetAllGenresQuery();
        var genres = await _mediator.Send(query);
        return Ok(genres);
    }
    
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType<Error>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Error>(StatusCodes.Status404NotFound)]
    [HttpPost]
    public async Task<ActionResult<GenreFullInfo>> CreateGenre(GenreInfo info)
    {
        var command = new CreateGenreCommand(info);
        
        var result = await _mediator.Send(command);
        
        return result.IsFailure 
            ? StatusCode(result.Error!.Code, result.Error) 
            : CreatedAtAction(nameof(GetGenre), new {result.Data!.Id}, result.Data);
    }
}