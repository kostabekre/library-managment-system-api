using FluentValidation;
using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Models;
using LibraryManagementSystemAPI.Validators;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Commands;

internal sealed class UpdateBookHandler(IBookRepository bookRepository,
    IValidator<BookUpdateDto> validator) 
    : IRequestHandler<UpdateBookCommand, Error?>
{
    public async ValueTask<Error?> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request.Dto);
        if (validationResult.IsValid == false)
        {
            return Error.BadRequest(validationResult.GetErrorMessages());
        }
        BookUpdateDto bookUpdateDto = request.Dto;
        bool updated = await bookRepository.UpdateBookAsync(request.Id, bookUpdateDto);
        return !updated ? Error.NotFound() : null;
    }
}