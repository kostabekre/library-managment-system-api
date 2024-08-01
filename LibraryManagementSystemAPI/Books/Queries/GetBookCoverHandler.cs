using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Queries;

internal sealed class GetBookCoverHandler(IBookRepository bookRepository)
    : IRequestHandler<GetBookCoverQuery, Result<BookCoverDTO?>>
{
    public async ValueTask<Result<BookCoverDTO?>> Handle(GetBookCoverQuery request, CancellationToken cancellationToken)
    {
        return await bookRepository.GetCoverAsync(request.Id);
    }
}