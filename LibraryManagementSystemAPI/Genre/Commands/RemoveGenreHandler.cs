using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Genre.Commands;

internal sealed class RemoveGenreHandler : IRequestHandler<RemoveGenreCommand, Error?>
{
    private readonly IGenreRepository _genreRepository;

    public RemoveGenreHandler(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async ValueTask<Error?> Handle(RemoveGenreCommand request, CancellationToken cancellationToken)
    {
        var deleted = await _genreRepository.RemoveGenreAsync(request.Id);

        if (deleted == false)
        {
            return new Error(404, new []{"Not Found"});
        }

        return null;
    }
}