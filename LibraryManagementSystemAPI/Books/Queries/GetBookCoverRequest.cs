using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Queries;

public record GetBookCoverQuery(int Id) : IRequest<Result<BookCoverDTO?>>;
internal sealed class GetBookCoverRequest : IRequestHandler<GetBookCoverQuery, Result<BookCoverDTO?>>
{
    private readonly IBookRepository _bookRepository;

    public GetBookCoverRequest(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async ValueTask<Result<BookCoverDTO?>> Handle(GetBookCoverQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _bookRepository.GetCoverAsync(request.Id);
            return result;
        }
        catch (Exception e)
        {
            return new Error(500, new[] { "Internal Error", e.Message });
        }
    }
}