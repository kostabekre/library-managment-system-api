using FluentValidation;
using LibraryManagementSystemAPI.Genre.Data;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemAPI.Genre;

[ApiController]
[Route("api/[controller]")]
public class GenresController : ControllerBase 
{
    private readonly IGenreRepository _genreRepository;
    private readonly IValidator<GenreInfo> _validator;

    public GenresController(IGenreRepository genreRepository, IValidator<GenreInfo> validator)
    {
        _genreRepository = genreRepository;
        _validator = validator;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<GenreFullInfo>> GetGenre(int id)
    {
        GenreFullInfo? genre = await _genreRepository.GetGenre(id);
        if (genre == null)
        {
            return NotFound();
        }
        return Ok(genre);
    }
    
    [HttpDelete]
    public async Task<ActionResult> RemoveGenre(int id)
    {
        bool deleted = await _genreRepository.RemoveGenre(id);
        if (!deleted)
        {
            return NotFound();
        }
        return Ok();
    }
    
    
    [HttpGet]
    [Route("get_all")]
    public async Task<ActionResult<IEnumerable<GenreFullInfo>>> GetAllGenres()
    {
        var genres = await _genreRepository.GetAllGenre();
        return Ok(genres);
    }
    
    [HttpPost]
    public async Task<ActionResult<GenreFullInfo>> CreateGenre(GenreInfo info)
    {
        var validationResult = await _validator.ValidateAsync(info);
        if (validationResult.IsValid == false)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }
        var fullInfo = await _genreRepository.CreateGenre(info);
        return CreatedAtAction(nameof(GetGenre), new {fullInfo.Id}, fullInfo);
    }
}