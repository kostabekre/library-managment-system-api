using System.Text.Json;
using LibraryManagementSystemAPI.Books.Commands;
using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Books.Queries;
using LibraryManagementSystemAPI.Models;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemAPI.Books;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IMediator _mediator;

    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BookShortInfo>>> GetAllBooksShortInfo()
    {
        var query = new GetAllBooksShortInfoQuery();
        
        var result = await _mediator.Send(query);
        
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BookShortInfo>>> GetBooksShortInfo([FromQuery]BookParameters parameters)
    {
        var query = new GetAllBooksShortInfoPageListQuery(parameters);
        var result = await _mediator.Send(query);
        
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

    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<BookInfo>> GetBook(int id)
    {
        var query = new GetBookQuery(id);
        var result = await _mediator.Send(query);

        var book = result.Data;
        
        return book == null ? NotFound() : Ok(book);
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType<Error>(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<int>> CreateBook(BookCreateDto bookDto)
    {
        var command = new CreateBookCommand(bookDto);
        var result = await _mediator.Send(command);
        return result.IsFailure
            ? BadRequest(result.Error)
            : CreatedAtAction(nameof(GetBook), new { Id = result.Data }, result.Data);
    }
    
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType<Error>(StatusCodes.Status400BadRequest)]
    [HttpPost]
    [Route("cover")]
    public async Task<ActionResult> CreateBookWithCover(BookWithCoverCreateDto bookWithCover)
    {
        var command = new CreateBookWithCoverCommand(bookWithCover);
        var result = await _mediator.Send(command);
        return result.IsFailure 
            ? BadRequest(result.Error) 
            : CreatedAtAction(nameof(GetBook), new { Id = result.Data }, result.Data);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Error>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Error>(StatusCodes.Status404NotFound)]
    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> UpdateBook(int id, BookUpdateDto book)
    {
        var command = new UpdateBookCommand(id, book);
        
        var error = await _mediator.Send(command);
        
        return error != null ? StatusCode(error.Code, error) : Ok();
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> RemoveBook(int id)
    {
        var command = new RemoveBookCommand(id);
        
        var error = await _mediator.Send(command);

        return error != null ? NotFound() : Ok();
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Error>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Error>(StatusCodes.Status404NotFound)]
    [HttpPut]
    [Route("amount/{id}")]
    public async Task<ActionResult> UpdateBookAmount(int id, [FromForm]int amount)
    {
        var command = new UpdateBookAmountCommand(id, amount);
        
        var error = await _mediator.Send(command);
        
        return error != null ? StatusCode(error.Code, error) : Ok();
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Error>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Error>(StatusCodes.Status404NotFound)]
    [HttpPut]
    [Route("rating/{id}")]
    public async Task<ActionResult> UpdateBookRating(int id, [FromForm]int rating)
    {
        var command = new UpdateBookRatingCommand(id, rating);
        
        var error = await _mediator.Send(command);
        
        return error != null ? StatusCode(error.Code, error) : Ok();
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Error>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Error>(StatusCodes.Status404NotFound)]
    [HttpPut]
    [Route("cover/{id}")]
    public async Task<ActionResult> UpdateCover(int id, IFormFile file)
    {
        var command = new UpdateBookCoverCommand(id, file);

        var error = await _mediator.Send(command);

        if (error != null)
        {
            return StatusCode(error.Code, error);
        }

        return Ok();
    }
    
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    [Route("cover/{id}")]
    public async Task<ActionResult> GetCover(int id)
    {
        var query = new GetBookCoverQuery(id);
        var result = await _mediator.Send(query);

        var bookCover = result.Data;

        if (bookCover == null)
        {
            return NotFound();
        }
        
        Response.Headers.Append("Content-Disposition", bookCover.CD.ToString());

        return File(bookCover.File, "application/jpeg");
    }
}