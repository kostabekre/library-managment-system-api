using LibraryManagementSystemAPI.Context;
using LibraryManagementSystemAPI.Genre.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystemAPI.Genre;

public class EfCoreGenreRepository : IGenreRepository
{
    private readonly BookContext _bookContext;

    public EfCoreGenreRepository(BookContext bookContext)
    {
        _bookContext = bookContext;
    }

    public async Task<GenreFullInfo> CreateGenre(GenreInfo info)
    {
        var genre = (Data.Genre)info;
        _bookContext.Genres.Add(genre);
        await _bookContext.SaveChangesAsync();
        return (GenreFullInfo)genre;
    }

    public async Task<GenreFullInfo?> GetGenre(int id)
    {
        var genre = await _bookContext.Genres
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.Id == id);
        
        if (genre == null)
        {
            return null;
        }
        
        return (GenreFullInfo)genre;
    }

    public async Task<IEnumerable<GenreFullInfo>> GetAllGenre() => await _bookContext.Genres
        .AsNoTracking()
        .Select(g => (GenreFullInfo)g)
        .ToListAsync();

    public async Task<bool> RemoveGenre(int id)
    {
        var rowsDeleted = await _bookContext.Genres.Where(g => g.Id == id).ExecuteDeleteAsync();
        return rowsDeleted > 0;
    }

    public async Task<bool> IsNameUnique(string name) =>
        await _bookContext.Genres.AnyAsync(g => g.Name == name) == false;
}