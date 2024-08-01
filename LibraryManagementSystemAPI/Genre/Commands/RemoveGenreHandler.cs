using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Genre.Commands;

internal sealed class RemoveGenreHandler(IGenreRepository genreRepository) : IRequestHandler<RemoveGenreCommand, Error?>
{
    public async ValueTask<Error?> Handle(RemoveGenreCommand request, CancellationToken cancellationToken)
    {
        var deleted = await genreRepository.RemoveGenreAsync(request.Id);

        return deleted == false ? Error.NotFound() : null;
    }
}