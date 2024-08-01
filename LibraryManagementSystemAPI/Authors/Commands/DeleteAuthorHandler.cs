using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Authors.Commands;

internal sealed class DeleteAuthorHandler(IAuthorRepository authorRepository)
    : IRequestHandler<DeleteAuthorCommand, Error?>
{
    public async ValueTask<Error?> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var deleted = await authorRepository.DeleteAuthorAsync(request.Id);
        if (deleted == false)
        {
            return Error.NotFound();
        }

        return null;
    }
}