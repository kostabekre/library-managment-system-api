using LibraryManagementSystemAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorsController(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthors()
    {
        IEnumerable<Author> author = await _authorRepository.GetAllAuthors();
        return Ok(author);
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Author>> GetAuthor(int id)
    {
        Author? author = await _authorRepository.GetAuthor(id);
        if (author == null)
        {
            return NotFound();
        }
        return Ok(author);
    }

    [HttpPost]
    public async Task<ActionResult<Author>> CreateAuthor(Author author)
    {
        await _authorRepository.CreateAuthor(author);
        return CreatedAtAction(nameof(GetAuthor), new {author.Id}, author);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<Author>> UpdateAuthor(int id, Author author)
    {
        bool updated = await _authorRepository.UpdateAuthor(id, author);
        if (updated == false)
        {
            return NotFound();
        }

        return author;
    }
    

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteAuthor(int id)
    {
        bool deleted = await _authorRepository.DeleteAuthor(id);
        if (deleted == false)
        {
            return NotFound();
        }

        return Ok();
    }
}