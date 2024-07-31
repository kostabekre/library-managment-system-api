using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Authors.Commands;

internal sealed class DeleteAuthorHandler : IRequestHandler<DeleteAuthorCommand, Error?>
{
    private readonly IAuthorRepository _authorRepository;

    public DeleteAuthorHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async ValueTask<Error?> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var deleted = await _authorRepository.DeleteAuthorAsync(request.Id);
        if (deleted == false)
        {
            return new Error(404, new[] { "Not Found" });
        }

        return null;
    }
}