using FluentValidation;
using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Commands;

internal sealed class CreateBookHandler : IRequestHandler<CreateBookCommand, Result<int>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IValidator<BookCreateDto> _bookValidator;

    public CreateBookHandler(IValidator<BookCreateDto> bookValidator, IBookRepository bookRepository)
    {
        _bookValidator = bookValidator;
        _bookRepository = bookRepository;
    }

    public async ValueTask<Result<int>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var validationResult = _bookValidator.Validate(request.BookCreateDto);
        
        if (validationResult.IsValid == false)
        {
            return new Error(401, validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }

        try
        {
            return await _bookRepository.CreateBookAsync(request.BookCreateDto);
        }
        catch (Exception e)
        {
            return new Error(500, new[] { "Internal Error", e.Message });
        }
    }
}