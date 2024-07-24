using System.Net;
using System.Text.Json;
using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystemAPI;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _bookRepository;
    private readonly ILogger<BooksController> _logger;

    public BooksController(IBookRepository bookRepository, ILogger<BooksController> logger)
    {
        _bookRepository = bookRepository;
        _logger = logger;
    }

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IEnumerable<BookShortInfo>>> GetAllBooksShortInfo()
    {
        var books = await _bookRepository.GetAllBooksShortInfo();
        return Ok(books);
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<BookShortInfo>>> GetBooksShortInfo([FromQuery]BookParameters parameters)
    {
        var books = await _bookRepository.GetBooksShortInfo(parameters);
        var metadata = new
        {
            books.TotalPages,
            books.TotalCount,
            books.PageNumber,
            books.PageSize,
            books.HasNext,
            books.HasPrevious
        };
        
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));
        _logger.LogInformation($"Logged {books.TotalCount} from database");
        return Ok(books.Data);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<BookInfo>> GetBook(int id)
    {
        var book = await _bookRepository.GetBook(id);
        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    
    [HttpPut]
    [Route("cover/{id}")]
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
    [Route("cover/{id}")]
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
    [HttpPost]
    public async Task<ActionResult> CreateBook(BookCreateDTO book)
    {

        int createdId;
        try
        {
            createdId = await _bookRepository.CreateBook(book);
        }
        catch (DbUpdateException e)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(GetBook), new { Id = createdId }, createdId);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<BookInfo>> UpdateBook(int id, BookUpdateDTO book)
    {
        bool updated = await _bookRepository.UpdateBook(id, book);
        if (!updated)
        {
            return NotFound();
        }

        return Ok(book);
    }
    
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> RemoveBook(int id)
    {
        bool deleted = await _bookRepository.RemoveBook(id);
        if (!deleted)
        {
            return NotFound();
        }

        return Ok();
    }
}