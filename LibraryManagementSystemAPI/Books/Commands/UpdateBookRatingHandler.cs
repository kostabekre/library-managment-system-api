using LibraryManagementSystemAPI.Models;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystemAPI.Books.Commands;

internal sealed class UpdateBookRatingHandler(IBookRepository bookRepository)
    : IRequestHandler<UpdateBookRatingCommand, Error?>
{
    public async ValueTask<Error?> Handle(UpdateBookRatingCommand request, CancellationToken cancellationToken)
    {
        var updated = await bookRepository.UpdateBookRatingAsync(request.Id, request.Rating);

        if (updated == false)
        {
            return Error.NotFound();
        }

        return null;
    }
}