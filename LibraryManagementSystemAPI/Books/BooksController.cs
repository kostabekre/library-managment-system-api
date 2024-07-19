using System.Text.Json;
using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Models;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<PagedList<BookShortInfo>>> GetBooksShortInfo(BookParameters parameters)
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

    [HttpPost]
    public async Task<ActionResult> CreateBook(BookCreateDTO book)
    {
        var createdId = await _bookRepository.CreateBook(book);

        return CreatedAtAction(nameof(GetBook), new { createdId }, createdId);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<BookInfo>> UpdateBook(int id, BookInfo book)
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