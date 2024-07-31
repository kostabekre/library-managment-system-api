using LibraryManagementSystemAPI.Authors.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Authors.Queries;

internal sealed class GetAllAuthorsHandler : IRequestHandler<GetAllAuthorsQuery, IEnumerable<AuthorFullInfo>>
{
    private readonly IAuthorRepository _authorRepository;

    public GetAllAuthorsHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async ValueTask<IEnumerable<AuthorFullInfo>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
    {
        return await _authorRepository.GetAllAuthorsAsync();
    }
}