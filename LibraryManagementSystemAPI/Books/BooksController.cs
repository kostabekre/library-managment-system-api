using System.Text.Json;
using LibraryManagementSystemAPI.Books.Commands;
using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Books.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemAPI.Books;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IMediator _mediator;

    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IEnumerable<BookShortInfo>>> GetAllBooksShortInfo()
    {
        var query = new GetAllBooksShortInfoQuery();
        var result = await _mediator.Send(query);
        if (result.IsFailure)
        {
            return StatusCode(result.Error!.Code, result.Error.Messages);
        }
        return Ok(result.Data);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookShortInfo>>> GetBooksShortInfo([FromQuery]BookParameters parameters)
    {
        var query = new GetAllBooksShortInfoPageListQuery(parameters);
        var result = await _mediator.Send(query);
        if (result.IsFailure)
        {
            return StatusCode(result.Error!.Code, result.Error.Messages);
        }
        
        var books = result.Data!;
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
        return Ok(books);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<BookInfo>> GetBook(int id)
    {
        var query = new GetBookQuery(id);
        var result = await _mediator.Send(query);
        if (result.IsFailure)
        {
            return StatusCode(result.Error.Code, result.Error.Messages);
        }

        var book = result.Data;
        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateBook(BookCreateDto bookDto)
    {
        var command = new CreateBookCommand(bookDto);
        var result = await _mediator.Send(command);
        if (result.IsFailure)
        {
            return StatusCode(result.Error!.Code, result.Error.Messages);
        }

        return CreatedAtAction(nameof(GetBook), new { Id = result.Data }, result.Data);
    }
    
    [HttpPost]
    [Route("cover")]
    public async Task<ActionResult> CreateBookWithCover(BookWithCoverCreateDto bookWithCover)
    {
        var command = new CreateBookWithCoverCommand(bookWithCover);
        var result = await _mediator.Send(command);
        if (result.IsFailure)
        {
            return StatusCode(result.Error.Code, result.Error.Messages);
        }

        return CreatedAtAction(nameof(GetBook), new { Id = result.Data }, result.Data);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> UpdateBook(int id, BookUpdateDto book)
    {
        var command = new UpdateBookCommand(id, book);
        var error = await _mediator.Send(command);
        if (error != null)
        {
            return StatusCode(error.Code, error.Messages);
        }

        return Ok();
    }
    
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> RemoveBook(int id)
    {
        var command = new RemoveBookCommand(id);
        var error = await _mediator.Send(command);
        
        if (error != null)
        {
            return StatusCode(error.Code, error.Messages);
        }

        return Ok();
    }

    [HttpPut]
    [Route("amount/{id}")]
    public async Task<ActionResult> UpdateBookAmount(int id, [FromForm]int amount)
    {
        var command = new UpdateBookAmountCommand(id, amount);
        var error = await _mediator.Send(command);
        if (error != null)
        {
            return StatusCode(error.Code, error.Messages);
        }

        return Ok();
    }
    
    [HttpPut]
    [Route("rating/{id}")]
    public async Task<ActionResult> UpdateBookRating(int id, [FromForm]int rating)
    {
        var command = new UpdateBookRatingCommand(id, rating);
        var error = await _mediator.Send(command);
        if (error != null)
        {
            return StatusCode(error.Code, error.Messages);
        }

        return Ok();
    }
    
    [HttpPut]
    [Route("cover/{id}")]
    public async Task<ActionResult> UpdateCover(int id, IFormFile file)
    {
        var command = new UpdateBookCoverCommand(id, file);

        var error = await _mediator.Send(command);

        if (error != null)
        {
            return StatusCode(error.Code, error.Messages);
        }

        return Ok();
    }
    
    [HttpGet]
    [Route("cover/{id}")]
    public async Task<ActionResult> GetCover(int id)
    {
        var query = new GetBookCoverQuery(id);
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return StatusCode(result.Error.Code, result.Error.Messages);
        }

        var bookCover = result.Data;

        if (bookCover == null)
        {
            return NotFound();
        }
        
        Response.Headers.Append("Content-Disposition", bookCover.CD.ToString());

        return File(bookCover.File, "application/jpeg");
    }
}