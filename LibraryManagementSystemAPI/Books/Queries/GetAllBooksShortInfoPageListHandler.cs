using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Data;
using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Queries;

internal sealed class
    GetAllBooksShortInfoPageListHandler : IRequestHandler<GetAllBooksShortInfoPageListQuery, Result<PagedList<BookShortInfo>>>
{
    private readonly IBookRepository _bookRepository;
    private readonly ILogger<BooksController> _logger;

    public GetAllBooksShortInfoPageListHandler(IBookRepository bookRepository, ILogger<BooksController> logger)
    {
        _bookRepository = bookRepository;
        _logger = logger;
    }

    public async ValueTask<Result<PagedList<BookShortInfo>>> Handle(GetAllBooksShortInfoPageListQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _bookRepository.GetBooksShortInfoAsync(request.Parameters);
            return result;
        }
        catch (Exception e)
        {
            return new Error(500, new []{e.Message});
        }

    }
}