using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemAPI.Genre;

[ApiController]
[Route("api/[controller]")]
public class GenresController : ControllerBase 
{
    private readonly IGenreRepository _genreRepository;

    public GenresController(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Genre>> GetGenre(int id)
    {
        Genre? genre = await _genreRepository.GetGenre(id);
        if (genre == null)
        {
            return NotFound();
        }
        return Ok(genre);
    }
    
    [HttpDelete]
    public async Task<ActionResult<Genre>> RemoveGenre(int id)
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
    public async Task<ActionResult<IEnumerable<Genre>>> GetAllGenres()
    {
        var genres = await _genreRepository.GetAllGenre();
        return Ok(genres);
    }
    
    [HttpPost]
    public async Task<ActionResult<Genre>> CreateGenre(Genre genre)
    {
        await _genreRepository.CreateGenre(genre);
        return CreatedAtAction(nameof(GetGenre), new {genre.Id}, genre);
    }
}