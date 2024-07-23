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
        .Select(b => new BookShortInfo(b.Id, b.Name, b.CoverPath, b.Authors.First().Name, b.Publisher.Name)).ToListAsync();

    public async Task<PagedList<IList<BookShortInfo>>> GetBooksShortInfo(BookParameters parameters)
    {
        var result = await _bookContext.Books
            .OrderBy(b => b.Name)
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .Include(b => b.Authors)
            .Include(b => b.Publisher)
            .Select(b => new BookShortInfo(b.Id, b.Name, b.CoverPath, b.Authors.First().Name, b.Publisher.Name))
            .ToListAsync();

        return new PagedList<IList<BookShortInfo>>(result, parameters.PageSize, parameters.PageNumber, result.Count);
    }

    public async Task<int> CreateBook(BookCreateDTO dto)
    {
        var book = BookCreateDTO.Convert(dto, null);
        _bookContext.Books.Add(book);
        await _bookContext.SaveChangesAsync();

        var bookAuthors = new List<BookAuthor>();
        foreach (var a in dto.AuthorsId)
        {
            var entity = new BookAuthor(){BookId = book.Id, AuthorId = a};
            bookAuthors.Add(entity);
        }

        book.BookAuthors = bookAuthors;

        var bookgenre = new List<BookGenre>();
        foreach (var g in dto.GenresId)
        {
            var genre = new BookGenre() { BookId = book.Id, GenreId = g};
            bookgenre.Add(genre);
        }

        book.BookGenres = bookgenre;
        _bookContext.Entry(book).State = EntityState.Modified;

        await _bookContext.SaveChangesAsync();
        return book.Id;
    }

    public async Task<BookInfo?> GetBook(int id)
    {
        var book = await _bookContext.Books
            .Include(b => b.Authors)
            .Include(b => b.Publisher)
            .Include(b => b.Genres)
            .FirstOrDefaultAsync(b => b.Id == id);
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

    public async Task<bool> UpdateBook(int id, BookUpdateDTO bookDTO)
    {
        var book = _bookContext.Books.Include(b => b.BookGenres).FirstOrDefault(b => b.Id == id);
        book.Name = bookDTO.Name;
        var bookGenres = book.BookGenres.ToList();
        bookGenres.Clear();
        book.BookGenres = bookDTO.GenresId.Select(id => new BookGenre() { BookId = id, GenreId = id}).ToList();

        await _bookContext.SaveChangesAsync();

        return true;
    }
}