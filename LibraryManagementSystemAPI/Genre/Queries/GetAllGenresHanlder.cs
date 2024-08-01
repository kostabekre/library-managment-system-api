using LibraryManagementSystemAPI.Genre.Data;
using Mediator;

namespace LibraryManagementSystemAPI.Genre.Queries;

internal sealed class GetAllGenresHanlder(IGenreRepository genreRepository)
    : IRequestHandler<GetAllGenresQuery, IEnumerable<GenreFullInfo>>
{
    public async ValueTask<IEnumerable<GenreFullInfo>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
    {
        return await genreRepository.GetAllGenreAsync();
    }
}