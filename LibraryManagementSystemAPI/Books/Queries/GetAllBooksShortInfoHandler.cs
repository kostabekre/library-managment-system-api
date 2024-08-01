using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Queries;

internal sealed class GetAllBooksShortInfoHandler(IBookRepository bookRepository)
    : IRequestHandler<GetAllBooksShortInfoQuery, IEnumerable<BookShortInfo>>
{
    public async ValueTask<IEnumerable<BookShortInfo>> Handle(GetAllBooksShortInfoQuery request, CancellationToken cancellationToken)
    {
        return await bookRepository.GetAllBooksShortInfoAsync();
    }
}