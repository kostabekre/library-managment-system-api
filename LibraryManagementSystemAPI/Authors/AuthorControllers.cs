using LibraryManagementSystemAPI.Authors.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemAPI.Authors;

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
    public async Task<ActionResult<IEnumerable<AuthorFullInfo>>> GetAllAuthors()
    {
        IEnumerable<AuthorFullInfo> authors = await _authorRepository.GetAllAuthors();
        return Ok(authors);
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<AuthorFullInfo>> GetAuthor(int id)
    {
        AuthorFullInfo? author = await _authorRepository.GetAuthor(id);
        if (author == null)
        {
            return NotFound();
        }
        return Ok(author);
    }

    [HttpPost]
    public async Task<ActionResult<AuthorFullInfo>> CreateAuthor(AuthorInfo author)
    {
        var fullInfo = await _authorRepository.CreateAuthor(author);
        return CreatedAtAction(nameof(GetAuthor), new {fullInfo.Id}, fullInfo);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> UpdateAuthor(int id, AuthorInfo author)
    {
        bool updated = await _authorRepository.UpdateAuthor(id, author);
        if (updated == false)
        {
            return NotFound();
        }

        return Ok();
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