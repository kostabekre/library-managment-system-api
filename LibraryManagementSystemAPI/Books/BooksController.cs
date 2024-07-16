using System.Text.Json;
using LibraryManagementSystemAPI.Books.Data;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemAPI;

[ApiController]
[Route("api/{controller}")]
public class BooksController : Controller
{

    private readonly IBookRepository _bookRepository;
    private readonly ILogger _logger;

    public BooksController(IBookRepository bookRepository, ILogger logger)
    {
        _bookRepository = bookRepository;
        _logger = logger;
    }

    [HttpGet]
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
    public async Task<ActionResult<BookInfo>> CreateBook(BookCreateModel model)
    {
        var book = await _bookRepository.CreateBook(model);

        return CreatedAtAction(nameof(CreateBook), book);
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