using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Data;
using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Queries;

internal sealed class
    GetAllBooksShortInfoPageListHandler(IBookRepository bookRepository)
    : IRequestHandler<GetAllBooksShortInfoPageListQuery, Result<PagedList<BookShortInfo>>>
{
    public async ValueTask<Result<PagedList<BookShortInfo>>> Handle(GetAllBooksShortInfoPageListQuery request, CancellationToken cancellationToken)
    {
        return await bookRepository.GetBooksShortInfoAsync(request.Parameters);
    }
}