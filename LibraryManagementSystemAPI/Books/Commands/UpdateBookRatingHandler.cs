using LibraryManagementSystemAPI.Models;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystemAPI.Books.Commands;

internal sealed class UpdateBookRatingHandler : IRequestHandler<UpdateBookRatingCommand, Error?>
{
    private readonly IBookRepository _bookRepository;

    public UpdateBookRatingHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async ValueTask<Error?> Handle(UpdateBookRatingCommand request, CancellationToken cancellationToken)
    {
        bool updated = false;

        try
        {
            updated = await _bookRepository.UpdateBookRatingAsync(request.Id, request.Rating);
        }
        catch (DbUpdateException e)
        {
            return new Error(404, new[] { "Not Founded" });
        }
        catch (Exception e)
        {
            return new Error(500, new[] { "Internal Error", e.Message });
        }

        return null;
    }
}