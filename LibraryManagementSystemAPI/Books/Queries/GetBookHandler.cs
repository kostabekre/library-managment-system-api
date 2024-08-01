using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Queries;

internal sealed class GetBookHandler(IBookRepository bookRepository) : IRequestHandler<GetBookQuery, Result<BookInfo?>>
{
    public async ValueTask<Result<BookInfo?>> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
        return await bookRepository.GetBookAsync(request.Id);
    }
}