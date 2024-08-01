using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Commands;

internal sealed class RemoveBookHandler(IBookRepository bookRepository) : IRequestHandler<RemoveBookCommand, Error?>
{
    public async ValueTask<Error?> Handle(RemoveBookCommand request, CancellationToken cancellationToken)
    {
        bool deleted = await bookRepository.RemoveBookAsync(request.Id);
        return deleted == false ? Error.NotFound() : null;
    }
}