using LibraryManagementSystemAPI.Authors.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Authors.Queries;

internal sealed class GetAllAuthorsHandler(IAuthorRepository authorRepository)
    : IRequestHandler<GetAllAuthorsQuery, IEnumerable<AuthorFullInfo>>
{
    public async ValueTask<IEnumerable<AuthorFullInfo>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
    {
        return await authorRepository.GetAllAuthorsAsync();
    }
}