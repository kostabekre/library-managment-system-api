using LibraryManagementSystemAPI.Authors.Models;

namespace LibraryManagementSystemAPI.Authors;

public interface IAuthorRepository
{
    Task<AuthorFullInfo?> GetAuthor(int id);
    Task<AuthorFullInfo> CreateAuthor(AuthorInfo info);
    Task<bool> DeleteAuthor(int id);
    Task<bool> UpdateAuthor(int id, AuthorInfo author);
    Task<IEnumerable<AuthorFullInfo>> GetAllAuthors();
    Task<bool> IsNameUnique(string name);
}