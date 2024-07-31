using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Queries;

internal sealed class GetAllBooksShortInfoHandler : IRequestHandler<GetAllBooksShortInfoQuery, Result<IEnumerable<BookShortInfo>>>
{
    private readonly IBookRepository _bookRepository;

    public GetAllBooksShortInfoHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async ValueTask<Result<IEnumerable<BookShortInfo>>> Handle(GetAllBooksShortInfoQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _bookRepository.GetAllBooksShortInfoAsync();
            return new Result<IEnumerable<BookShortInfo>>(result);
        }
        catch (Exception e)
        {
            return new Error(500, new[]{e.Message});
        }
    }
}