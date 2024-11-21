using LibraryManagementSystemAPI.Books.Data;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Queries;

public sealed class GetAllBooksShortInfoByPublisherHandler : IRequestHandler<GetAllBooksShortInfoByPublisherQuery,
    IEnumerable<BookShortInfo>>
{
    private readonly IBookRepository _bookRepository;

    public GetAllBooksShortInfoByPublisherHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    public async ValueTask<IEnumerable<BookShortInfo>> Handle(GetAllBooksShortInfoByPublisherQuery request,
        CancellationToken cancellationToken)
    {
        return await _bookRepository.GetAllBooksShortInfoByPublisherIdAsync(request.PublisherId);
    }
}