using FluentValidation;
using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Models;
using LibraryManagementSystemAPI.Validators;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Commands;

internal sealed class CreateBookHandler(IValidator<BookCreateDto> bookValidator, IBookRepository bookRepository)
    : IRequestHandler<CreateBookCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await bookValidator.ValidateAsync(request.BookCreateDto);
        
        if (validationResult.IsValid == false)
        {
            return Error.BadRequest(validationResult.GetErrorMessages());
        }

        return await bookRepository.CreateBookAsync(request.BookCreateDto);
    }
}