using LibraryManagementSystemAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystemAPI.Genre;

public class EfCoreGenreRepository : IGenreRepository
{
    private readonly BookContext _bookContext;

    public EfCoreGenreRepository(BookContext bookContext)
    {
        _bookContext = bookContext;
    }

    public async Task CreateGenre(Genre genre)
    {
        _bookContext.Genres.Add(genre);
        await _bookContext.SaveChangesAsync();
    }

    public async Task<Genre?> GetGenre(int id)
    {
        var genre = await _bookContext.Genres
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.Id == id);
        return genre;
    }

    public async Task<IEnumerable<Genre>> GetAllGenre() => await _bookContext.Genres
        .AsNoTracking()
        .ToListAsync();

    public async Task<bool> RemoveGenre(int id)
    {
        var rowsDeleted = await _bookContext.Genres.Where(g => g.Id == id).ExecuteDeleteAsync();
        return rowsDeleted > 0;
    }
}