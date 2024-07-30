using LibraryManagementSystemAPI.Genre.Data;

namespace LibraryManagementSystemAPI.Genre;

public interface IGenreRepository
{
    Task<GenreFullInfo> CreateGenre(GenreInfo info);
    Task<GenreFullInfo?> GetGenre(int id);
    Task<IEnumerable<GenreFullInfo>> GetAllGenre();
    Task<bool> RemoveGenre(int id);
    Task<bool> IsNameUnique(string name);
}