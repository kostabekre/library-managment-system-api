using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Models;

namespace LibraryManagementSystemAPI;

public interface IBookRepository
{
    Task<IEnumerable<BookShortInfo>> GetAllBooksShortInfo();
    Task<PagedList<IList<BookShortInfo>>> GetBooksShortInfo(BookParameters parameters);
    Task<int> CreateBook(BookCreateDTO model);
    Task<BookInfo?> GetBook(int id);
    Task<bool> RemoveBook(int id);
    Task<bool> UpdateBook(int id, BookUpdateDTO bookDTO);
    Task<BookCoverDTO?> GetCover(int id);
    Task<bool> UpdateCover(int id, IFormFile file);
}