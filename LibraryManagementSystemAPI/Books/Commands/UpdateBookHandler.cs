using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Commands;

internal sealed class UpdateBookHandler(IBookRepository bookRepository) : IRequestHandler<UpdateBookCommand, Error?>
{
    public async ValueTask<Error?> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        BookUpdateDto bookUpdateDto = request.Dto;
        bool updated = await bookRepository.UpdateBookAsync(request.Id, bookUpdateDto);
        return !updated ? Error.NotFound() : null;
    }
}