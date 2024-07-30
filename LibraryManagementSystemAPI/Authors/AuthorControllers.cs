using FluentValidation;
using LibraryManagementSystemAPI.Authors.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemAPI.Authors;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IValidator<AuthorInfo> _validator;

    public AuthorsController(IAuthorRepository authorRepository, IValidator<AuthorInfo> validator)
    {
        _authorRepository = authorRepository;
        _validator = validator;
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
    public async Task<ActionResult<AuthorFullInfo>> CreateAuthor(AuthorInfo info)
    {
        var validationResult = await _validator.ValidateAsync(info);
        if (validationResult.IsValid == false)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }
        var fullInfo = await _authorRepository.CreateAuthor(info);
        return CreatedAtAction(nameof(GetAuthor), new {fullInfo.Id}, fullInfo);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> UpdateAuthor(int id, AuthorInfo info)
    {
        var validationResult = await _validator.ValidateAsync(info);
        if (validationResult.IsValid == false)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }
        
        bool updated = await _authorRepository.UpdateAuthor(id, info);
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