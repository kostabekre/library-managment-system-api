using System.Net.Mime;
using LibraryManagementSystemAPI.Books.CoverValidation;
using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Context;
using LibraryManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystemAPI.Books;

public class EfCoreBookRepository : IBookRepository
{
    private readonly BookContext _bookContext;
    private readonly ICoverValidation _coverValidation;

    public EfCoreBookRepository(BookContext bookContext, ICoverValidation coverValidation)
    {
        _bookContext = bookContext;
        _coverValidation = coverValidation;
    }

    public async Task<IEnumerable<BookShortInfo>> GetAllBooksShortInfo() => await _bookContext.Books
        .AsNoTracking()
        .Include(b => b.Publisher)
        .Include(b => b.Authors)
        .Select(b => new BookShortInfo(b.Id, b.Name,b.Authors!.First().Name, b.Publisher!.Name))
        .ToListAsync();

    public async Task<PagedList<IList<BookShortInfo>>> GetBooksShortInfo(BookParameters parameters)
    {
        var result = await _bookContext.Books
            .AsNoTracking()
            .OrderBy(b => b.Name)
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .Include(b => b.Authors)
            .Include(b => b.Publisher)
            .Select(b => new BookShortInfo(b.Id, b.Name, b.Authors!.First().Name, b.Publisher!.Name))
            .ToListAsync();

        return new PagedList<IList<BookShortInfo>>(result, parameters.PageSize, parameters.PageNumber, result.Count);
    }

    public async Task<int> CreateBook(BookCreateDTO dto)
    {
        var book = BookCreateDTO.Convert(dto);

        var bookId = await SaveBook(dto, book);
        return bookId;
    }
    public async Task<int> CreateBookWithCover(BookWithCoverCreateDto dto)
    {
        var isCoverValid = _coverValidation.IsFileValid(dto.Cover);
        
        var book = BookWithCoverCreateDto.Convert(dto, isCoverValid.Result, isCoverValid.Name);

        return await SaveBook(dto.MainInfo, book);
    }

    private async Task<int> SaveBook(BookCreateDTO dto, Book book)
    {
        _bookContext.Books.Add(book);
        
        await _bookContext.SaveChangesAsync();

        var bookAuthors = new List<BookAuthor>();
        foreach (var a in dto.AuthorsId)
        {
            var entity = new BookAuthor(){BookId = book.Id, AuthorId = a};
            bookAuthors.Add(entity);
        }

        book.BookAuthors = bookAuthors;

        var bookGenres = new List<BookGenre>();
        foreach (var g in dto.GenresId)
        {
            var genre = new BookGenre() { BookId = book.Id, GenreId = g};
            bookGenres.Add(genre);
        }

        book.BookGenres = bookGenres;
        _bookContext.Entry(book).State = EntityState.Modified;

        await _bookContext.SaveChangesAsync();
        return book.Id;
    }


    public async Task<BookInfo?> GetBook(int id)
    {
        var book = await _bookContext.Books
            .AsNoTracking()
            .Include(b => b.Authors)
            .Include(b => b.Publisher)
            .Include(b => b.Genres)
            .Include(b =>b.Amount)
            .Include(b =>b.Rating)
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

    public async Task<bool> UpdateBook(int id, BookUpdateDTO bookDto)
    {
        var book = _bookContext.Books.Include(b => b.BookGenres).FirstOrDefault(b => b.Id == id);
        if (book == null)
        {
            return false;
        }
        
        book.Name = bookDto.Name;
        
        if (book.BookGenres != null)
        {
            var bookGenres = book.BookGenres.ToList();
            bookGenres.Clear();
        }

        book.BookGenres = bookDto.GenresId.Select(genreId => new BookGenre() { BookId = id, GenreId = genreId}).ToList();

        await _bookContext.SaveChangesAsync();

        return true;
    }

    public async Task<BookCoverDTO?> GetCover(int id)
    {
        var cover = await _bookContext.Set<BookCover>()
            .AsNoTracking()
            .Where(c => c.BookId == id)
            .FirstOrDefaultAsync();

        if (cover == null)
        {
            return null;
        }

        var cd = new ContentDisposition()
        {
            FileName = cover.Name,
            Inline = true
        };

        return new BookCoverDTO(){CD = cd, File = cover.CoverFile};
    }

    public async Task<Error?> UpdateCover(int id, IFormFile file)
    {
        var isCoverValid = _coverValidation.IsFileValid(file);
        if (!isCoverValid.IsValid)
        {
            return new Error(400, isCoverValid.Message);
        }

        string newName = $"{Guid.NewGuid()}.jpg";

        var rowsUpdated = await _bookContext.Set<BookCover>()
            .Where(c => c.BookId == id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(c => c.Name, newName)
                .SetProperty(c => c.CoverFile, isCoverValid.Result)
                .SetProperty(c => c.BookId, id));

        if (rowsUpdated > 0)
        {
            return null;
        }

        var bookCover = new BookCover() { BookId = id,  CoverFile = isCoverValid.Result!, Name = newName  };
        _bookContext.Set<BookCover>().Add(bookCover);
        try
        {
            await _bookContext.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            return new Error(404, "Not Founded");
        }

        return null;
    }

    public async Task<Error?> UpdateBookRating(int id, int rating)
    {
        int rowsUpdated = await _bookContext.Set<BookRating>()
            .Where(r => r.BookId == id)
            .ExecuteUpdateAsync(parameters =>
                parameters.SetProperty(r => r.Rating, rating));

        if (rowsUpdated > 0)
        {
            return null;
        }
        
        var bookRating = new BookRating() { BookId = id,  Rating = rating };
        _bookContext.Set<BookRating>().Add(bookRating);
        try
        {
            await _bookContext.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            return new Error(404, "Not Founded");
        }

        return null;
    }

    public async Task<Error?> UpdateBookAmount(int id, int amount)
    {
        int rowsUpdated = await _bookContext.Set<BookAmount>()
            .Where(r => r.BookId == id)
            .ExecuteUpdateAsync(parameters =>
                parameters.SetProperty(r => r.Amount, amount));
        if (rowsUpdated > 0)
        {
            return null;
        }
        
        var bookAmount = new BookAmount() { BookId = id,  Amount = amount};
        _bookContext.Set<BookAmount>().Add(bookAmount);
        try
        {
            await _bookContext.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            return new Error(404, "Not Founded");
        }

        return null;
    }
}