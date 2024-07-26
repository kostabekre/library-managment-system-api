namespace LibraryManagementSystemAPI.Genre;

public interface IGenreRepository
{
    Task CreateGenre(Genre genre);
    Task<Genre?> GetGenre(int id);
    Task<IEnumerable<Genre>> GetAllGenre();
    Task<bool> RemoveGenre(int id);
}