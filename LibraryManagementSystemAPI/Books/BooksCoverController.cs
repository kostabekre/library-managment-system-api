using FluentValidation;
using LibraryManagementSystemAPI.Books.CoverValidation;
using LibraryManagementSystemAPI.Books.Data;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemAPI.Books;

[ApiController]
[Route("api/books/cover")]
public class BooksCoverController : ControllerBase
{
    private readonly IBookRepository _bookRepository;
    private readonly ILogger<BooksController> _logger;
    private readonly IValidator<CoverInfo> _validator;

    public BooksCoverController(IBookRepository bookRepository, ILogger<BooksController> logger, IValidator<CoverInfo> validator)
    {
        _bookRepository = bookRepository;
        _logger = logger;
        _validator = validator;
    }

    
    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> UpdateCover(int id, IFormFile file)
    {
        var validationResult = _validator.Validate(new CoverInfo(file));
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }
        
        var error = await _bookRepository.UpdateCover(id, file);

        if (error != null)
        {
            return StatusCode(error.Code, error.Messages);
        }

        return Ok();
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetCover(int id)
    {
        var result = await _bookRepository.GetCover(id);

        if (result == null)
        {
            return NotFound();
        }
        
        Response.Headers.Append("Content-Disposition", result.CD.ToString());

        return File(result.File, "application/jpeg");
    }
}