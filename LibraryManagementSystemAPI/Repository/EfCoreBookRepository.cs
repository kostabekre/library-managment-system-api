using LibraryManagementSystemAPI.Books.Data;

namespace LibraryManagementSystemAPI.Repository;

public class EfCoreBookRepository : IBookRepository
{
    public Task<IEnumerable<BookShortInfo>> GetAllBooksShortInfo()
    {
        throw new NotImplementedException();
    }

    public Task<PagedList<BookShortInfo>> GetBooksShortInfo(BookParameters parameters)
    {
        throw new NotImplementedException();
    }

    public Task<BookInfo> CreateBook(BookCreateModel model)
    {
        throw new NotImplementedException();
    }

    public Task<BookInfo?> GetBook(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveBook(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateBook(int id, BookInfo book)
    {
        throw new NotImplementedException();
    }
}