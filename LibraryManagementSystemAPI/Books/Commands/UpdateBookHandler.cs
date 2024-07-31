using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Commands;

internal sealed class UpdateBookHandler : IRequestHandler<UpdateBookCommand, Error?>
{
    private readonly IBookRepository _bookRepository;

    public UpdateBookHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async ValueTask<Error?> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        BookUpdateDto bookUpdateDto = request.Dto;
        bool updated = await _bookRepository.UpdateBookAsync(request.Id, bookUpdateDto);
        if (!updated)
        {
            return new Error(404, new[] { "Not Found" });
        }

        return null;
    }
}