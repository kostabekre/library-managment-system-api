using FluentValidation;
using LibraryManagementSystemAPI.Models;
using LibraryManagementSystemAPI.Validators;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystemAPI.Books.Commands;

internal sealed class UpdateBookRatingHandler(IBookRepository bookRepository,
    IValidator<UpdateBookRatingCommand> validator)
    : IRequestHandler<UpdateBookRatingCommand, Error?>
{
    public async ValueTask<Error?> Handle(UpdateBookRatingCommand request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);

        if (validationResult.IsValid == false)
        {
            return Error.BadRequest(validationResult.GetErrorMessages());
        }
        
        var updated = await bookRepository.UpdateBookRatingAsync(request.Id, request.Rating);

        if (updated == false)
        {
            return Error.NotFound();
        }

        return null;
    }
}