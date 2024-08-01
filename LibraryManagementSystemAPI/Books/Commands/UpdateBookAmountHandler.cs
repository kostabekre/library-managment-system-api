using LibraryManagementSystemAPI.Models;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystemAPI.Books.Commands;

internal sealed class UpdateBookAmountHandler(IBookRepository bookRepository)
    : IRequestHandler<UpdateBookAmountCommand, Error?>
{
    public async ValueTask<Error?> Handle(UpdateBookAmountCommand request, CancellationToken cancellationToken)
    {
        bool updated = await bookRepository.UpdateBookAmountAsync(request.Id, request.Amount);

        return updated == false ? Error.NotFound() : null;
    }
}