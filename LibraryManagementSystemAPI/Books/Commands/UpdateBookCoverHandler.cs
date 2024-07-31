using FluentValidation;
using LibraryManagementSystemAPI.Books.CoverValidation;
using LibraryManagementSystemAPI.Genre.Commands;
using LibraryManagementSystemAPI.Models;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystemAPI.Books.Commands;

internal sealed class UpdateBookCoverHandler : IRequestHandler<UpdateBookCoverCommand, Error?>
{
    private readonly IValidator<CoverInfo> _validator;
    private readonly IBookRepository _bookRepository;

    public UpdateBookCoverHandler(IBookRepository bookRepository, IValidator<CoverInfo> validator)
    {
        _bookRepository = bookRepository;
        _validator = validator;
    }

    public async ValueTask<Error?> Handle(UpdateBookCoverCommand request, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(new CoverInfo(request.FormFile));
        if (!validationResult.IsValid)
        {
            return new Error(401, validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }
        
        bool updated = false;

        try
        {
            updated = await _bookRepository.UpdateCoverAsync(request.Id, request.FormFile);
        }
        catch (DbUpdateException e)
        {
            return new Error(404, new[] { "Not Founded" });
        }
        catch (Exception e)
        {
            return new Error(500, new[] { "Internal Error", e.Message });
        }

        if (updated == false)
        {
            return new Error(404, new[] { "Not Founded" });
        }

        return null;
    }
}