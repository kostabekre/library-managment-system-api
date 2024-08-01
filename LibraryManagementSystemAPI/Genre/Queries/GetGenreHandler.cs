using LibraryManagementSystemAPI.Genre.Data;
using Mediator;

namespace LibraryManagementSystemAPI.Genre.Queries;

internal sealed class GetGenreHandler(IGenreRepository genreRepository) : IRequestHandler<GetGenreQuery, GenreFullInfo?>
{
    public async ValueTask<GenreFullInfo?> Handle(GetGenreQuery request, CancellationToken cancellationToken)
    {
        return await genreRepository.GetGenreAsync(request.Id);
    }
}