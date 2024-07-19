using LibraryManagementSystemAPI.Models;

namespace LibraryManagementSystemAPI.Controllers;

public interface IAuthorRepository
{
    Task<Author?> GetAuthor(int id);
    Task<Author> CreateAuthor(Author author);
    Task<bool> DeleteAuthor(int id);
    Task<bool> UpdateAuthor(int id, Author author);
    Task<IEnumerable<Author>> GetAllAuthors();
}