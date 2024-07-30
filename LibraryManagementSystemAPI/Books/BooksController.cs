using System.Text.Json;
using FluentValidation;
using LibraryManagementSystemAPI.Books.CoverValidation;
using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemAPI.Books;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _bookRepository;
    private readonly ILogger<BooksController> _logger;
    private readonly IValidator<BookCreateDto> _bookValidator;
    private readonly IValidator<CoverInfo> _bookCoverValidator;

    public BooksController(IBookRepository bookRepository,
        ILogger<BooksController> logger,
        IValidator<CoverInfo> bookCoverValidator,
        IValidator<BookCreateDto> bookValidator)
    {
        _bookRepository = bookRepository;
        _logger = logger;
        _bookCoverValidator = bookCoverValidator;
        _bookValidator = bookValidator;
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

    [HttpPost]
    public async Task<ActionResult> CreateBook(BookCreateDto bookDto)
    {
        var validationResult = _bookValidator.Validate(bookDto);

        if (validationResult.IsValid == false)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }
        
        var result = await _bookRepository.CreateBook(bookDto);

        if (result.IsFailure)
        {
            if (result.Error.Code == 401)
            {
                return BadRequest(result.Error.Messages);
            }
            
            return StatusCode(500);
        }

        return CreatedAtAction(nameof(GetBook), new { Id = result.Data }, result.Data);
    }
    
    [HttpPost]
    [Route("cover")]
    public async Task<ActionResult> CreateBookWithCover(BookWithCoverCreateDto bookWithCover)
    {
        var validationResult = _bookCoverValidator.Validate(new CoverInfo(bookWithCover.Cover));

        if (validationResult.IsValid == false)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }

        var createValidationResult = _bookValidator.Validate(bookWithCover.Details);

        if (createValidationResult.IsValid == false)
        {
            return BadRequest(createValidationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }
        
        var result = await _bookRepository.CreateBookWithCover(bookWithCover);

        if (result.IsFailure)
        {
            if (result.Error.Code == 401)
            {
                return BadRequest(result.Error.Messages);
            }
        }

        return CreatedAtAction(nameof(GetBook), new { Id = result.Data }, result.Data);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<BookInfo>> UpdateBook(int id, BookUpdateDto book)
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

    [HttpPut]
    [Route("amount/{id}")]
    public async Task<ActionResult> UpdateBookAmount(int id, [FromForm]int amount)
    {
        var error = await _bookRepository.UpdateBookAmount(id, amount);
        if (error != null)
        {
            return NotFound();
        }

        return Ok();
    }
    
    [HttpPut]
    [Route("rating/{id}")]
    public async Task<ActionResult> UpdateBookRating(int id, [FromForm]int rating)
    {
        var error = await _bookRepository.UpdateBookRating(id, rating);
        if (error != null)
        {
            return NotFound();
        }

        return Ok();
    }
}