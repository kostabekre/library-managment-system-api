using LibraryManagementSystemAPI.Genre.Data;

namespace LibraryManagementSystemAPI.Genre;

public interface IGenreRepository
{
    Task<GenreFullInfo> CreateGenreAsync(GenreInfo info);
    Task<GenreFullInfo?> GetGenreAsync(int id);
    Task<IEnumerable<GenreFullInfo>> GetAllGenreAsync();
    Task<bool> RemoveGenreAsync(int id);
    Task<bool> IsNameUniqueAsync(string name);
    Task<bool> AreGenresExistAsync(int[] ids);
}