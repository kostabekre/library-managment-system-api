using FluentValidation;
using LibraryManagementSystemAPI.Books.CoverValidation;
using LibraryManagementSystemAPI.Genre.Commands;
using LibraryManagementSystemAPI.Models;
using LibraryManagementSystemAPI.Validators;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystemAPI.Books.Commands;

internal sealed class UpdateBookCoverHandler(IBookRepository bookRepository, IValidator<CoverInfo> validator)
    : IRequestHandler<UpdateBookCoverCommand, Error?>
{
    public async ValueTask<Error?> Handle(UpdateBookCoverCommand request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(new CoverInfo(request.FormFile));
        if (!validationResult.IsValid)
        {
            return Error.BadRequest(validationResult.GetErrorMessages());
        }

        bool updated = await bookRepository.UpdateCoverAsync(request.Id, request.FormFile);

        return updated == false ? Error.NotFound() : null;
    }
}