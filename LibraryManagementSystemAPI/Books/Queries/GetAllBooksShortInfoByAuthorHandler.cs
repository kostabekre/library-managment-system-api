using LibraryManagementSystemAPI.Books.Data;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Queries;

public class GetAllBooksShortInfoByAuthorHandler : IRequestHandler<GetAllBooksShortInfoByAuthorQuery, IEnumerable<BookShortInfo>>
{
    private readonly IBookRepository _bookRepository;

    public GetAllBooksShortInfoByAuthorHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    public async ValueTask<IEnumerable<BookShortInfo>> Handle(GetAllBooksShortInfoByAuthorQuery request, CancellationToken cancellationToken)
    {
        return await _bookRepository.GetAllBooksShortInfoByAuthorIdAsync(request.AuthorId);
    }
}