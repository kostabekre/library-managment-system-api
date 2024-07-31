using LibraryManagementSystemAPI.Authors.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Authors.Queries;

internal sealed class GetAuthorHandler : IRequestHandler<GetAuthorQuery, AuthorFullInfo?>
{
    private readonly IAuthorRepository _authorRepository;

    public GetAuthorHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async ValueTask<AuthorFullInfo?> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
    {
        return await _authorRepository.GetAuthorAsync(request.Id);
    }
}