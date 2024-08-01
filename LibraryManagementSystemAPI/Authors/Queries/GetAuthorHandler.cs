using LibraryManagementSystemAPI.Authors.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Authors.Queries;

internal sealed class GetAuthorHandler(IAuthorRepository authorRepository)
    : IRequestHandler<GetAuthorQuery, AuthorFullInfo?>
{
    public async ValueTask<AuthorFullInfo?> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
    {
        return await authorRepository.GetAuthorAsync(request.Id);
    }
}