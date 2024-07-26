using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemAPI.Books;

[ApiController]
[Route("api/books/cover")]
public class BooksCoverController : ControllerBase
{
    private readonly IBookRepository _bookRepository;
    private readonly ILogger<BooksController> _logger;

    public BooksCoverController(IBookRepository bookRepository, ILogger<BooksController> logger)
    {
        _bookRepository = bookRepository;
        _logger = logger;
    }

    
    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> UpdateCover(int id, IFormFile file)
    {
        var error = await _bookRepository.UpdateCover(id, file);

        if (error != null)
        {
            return StatusCode(error.Code, error.Message);
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