using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Commands;

internal sealed class RemoveBookHandler : IRequestHandler<RemoveBookCommand, Error?>
{
    private readonly IBookRepository _bookRepository;

    public RemoveBookHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async ValueTask<Error?> Handle(RemoveBookCommand request, CancellationToken cancellationToken)
    {
        bool deleted = await _bookRepository.RemoveBookAsync(request.Id);
        if (deleted == false)
        {
            return new Error(404, new[] { "Not Found" });
        }

        return null;
    }
}