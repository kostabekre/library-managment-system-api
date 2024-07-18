using LibraryManagementSystemAPI.Books.Data;

namespace LibraryManagementSystemAPI;

public interface IBookRepository
{
    Task<IEnumerable<BookShortInfo>> GetAllBooksShortInfo();
    Task<PagedList<BookShortInfo>> GetBooksShortInfo(BookParameters parameters);
    Task<BookInfo> CreateBook(BookCreateModel model);
    Task<BookInfo?> GetBook(int id);
    Task<bool> RemoveBook(int id);
    Task<bool> UpdateBook(int id, BookInfo book);
}