using LibraryManagementSystemAPI.Genre.Data;
using Mediator;

namespace LibraryManagementSystemAPI.Genre.Queries;

internal class GetAllGenresHanlder : IRequestHandler<GetAllGenresQuery, IEnumerable<GenreFullInfo>>
{
    private readonly IGenreRepository _genreRepository;

    public GetAllGenresHanlder(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async ValueTask<IEnumerable<GenreFullInfo>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
    {
        return await _genreRepository.GetAllGenreAsync();
    }
}