using LibraryManagementSystemAPI.Authors.Models;

namespace LibraryManagementSystemAPI.Authors;

public interface IAuthorRepository
{
    Task<AuthorFullInfo?> GetAuthorAsync(int id);
    Task<AuthorFullInfo> CreateAuthorAsync(AuthorInfo info);
    Task<bool> DeleteAuthorAsync(int id);
    Task<bool> UpdateAuthorAsync(int id, AuthorInfo author);
    Task<IEnumerable<AuthorFullInfo>> GetAllAuthorsAsync();
    Task<bool> IsNameUniqueAsync(string name);
}