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
    [Route("{id}")]
    public async Task<ActionResult<Author>> GetAuthor(int id)
    {
        Author author = await _authorRepository.GetAuthor(id);
        return Ok(author);
    }

    [HttpPost]
    public async Task<ActionResult<Author>> CreateAuthor(Author author)
    {
        await _authorRepository.CreateAuthor(author);
        return CreatedAtAction(nameof(CreateAuthor), author);
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