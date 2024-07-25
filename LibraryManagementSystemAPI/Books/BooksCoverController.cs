using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemAPI;

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
        var updated = await _bookRepository.UpdateCover(id, file);

        if (updated == false)
        {
            return NotFound();
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