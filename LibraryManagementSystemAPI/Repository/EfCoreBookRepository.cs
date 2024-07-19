using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Context;
using LibraryManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystemAPI.Repository;

public class EfCoreBookRepository : IBookRepository
{
    private readonly BookContext _bookContext;

    public EfCoreBookRepository(BookContext bookContext)
    {
        _bookContext = bookContext;
    }

    public async Task<IEnumerable<BookShortInfo>> GetAllBooksShortInfo() => await _bookContext.Books
        .Include(b => b.Publisher)
        .Include(b => b.Authors)
        .Select(b => new BookShortInfo(b.Name, b.CoverPath, b.Authors.First().Name, b.Publisher.Name)).ToListAsync();

    public async Task<PagedList<IList<BookShortInfo>>> GetBooksShortInfo(BookParameters parameters)
    {
        var result = await _bookContext.Books
            .OrderBy(b => b.Name)
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .Include(b => b.Authors)
            .Include(b => b.Publisher)
            .Select(b => new BookShortInfo(b.Name, b.CoverPath, b.Authors.First().Name, b.Publisher.Name))
            .ToListAsync();

        return new PagedList<IList<BookShortInfo>>(result, parameters.PageSize, parameters.PageNumber, result.Count);
    }

    public async Task<BookInfo> CreateBook(BookCreateModel model)
    {
        var book = new Book()
        {
            Name = model.Name,
            PublisherId = model.PublisherId,
            Authors = model.AuthorsId.Select(id => new Author() { Id = id }),
            BookGenres = model.BookGenresId.Select(genreId => new BookGenre() { GenreId = genreId })
        };
        _bookContext.Books.Add(book);
        await _bookContext.SaveChangesAsync();
        return new BookInfo(book);
    }

    public async Task<BookInfo?> GetBook(int id)
    {
        var book = await _bookContext.Books.FirstOrDefaultAsync(b => b.Id == id);
        if (book == null)
        {
            return null;
        }
        return new BookInfo(book);
    }

    public async Task<bool> RemoveBook(int id)
    {
        var rowsDeleted = await _bookContext.Books.Where(b => b.Id == id).ExecuteDeleteAsync();
        return rowsDeleted > 0;
    }

    public async Task<bool> UpdateBook(int id, BookInfo book)
    {
        var rowsUpdated = await _bookContext.Books.Where(b => b.Id == id).ExecuteUpdateAsync(properties => properties
            .SetProperty(b => b.Name, book.Name));
        return rowsUpdated > 0;
    }
}