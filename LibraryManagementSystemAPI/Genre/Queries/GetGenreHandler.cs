using LibraryManagementSystemAPI.Genre.Data;
using Mediator;

namespace LibraryManagementSystemAPI.Genre.Queries;

internal sealed class GetGenreHandler : IRequestHandler<GetGenreQuery, GenreFullInfo?>
{
    private readonly IGenreRepository _genreRepository;

    public GetGenreHandler(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async ValueTask<GenreFullInfo?> Handle(GetGenreQuery request, CancellationToken cancellationToken)
    {
        return await _genreRepository.GetGenreAsync(request.Id);
    }
}