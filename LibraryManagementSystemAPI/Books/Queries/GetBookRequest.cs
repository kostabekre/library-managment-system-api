using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Queries;

internal sealed class GetBookRequest : IRequestHandler<GetBookQuery, Result<BookInfo?>>
{
    private readonly IBookRepository _bookRepository;

    public GetBookRequest(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async ValueTask<Result<BookInfo?>> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _bookRepository.GetBookAsync(request.Id);
        }
        catch (Exception e)
        {
            return new Error(500, new[] { "Internal Error", e.Message });
        }
    }
}