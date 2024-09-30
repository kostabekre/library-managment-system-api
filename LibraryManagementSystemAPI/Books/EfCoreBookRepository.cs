using System.Net.Mime;
using LibraryManagementSystemAPI.Authors.Models;
using LibraryManagementSystemAPI.Books.CoverValidation;
using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Context;
using LibraryManagementSystemAPI.Data;
using LibraryManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystemAPI.Books;

public class EfCoreBookRepository : IBookRepository
{
    private readonly BookContext _bookContext;

    public EfCoreBookRepository(BookContext bookContext)
    {
        _bookContext = bookContext;
    }

    public async Task<IEnumerable<BookShortInfo>> GetAllBooksShortInfoByPublisherIdAsync(int publisherId) => await _bookContext.Books
        .AsNoTracking()
        .Include(b => b.Publisher)
        .Include(b => b.Authors)
        .Where(b => b.Publisher.Id == publisherId)
        .Select(b => new BookShortInfo(b.Id, b.Name, new AuthorShortInfo(b.Authors!.First().Id, b.Authors!.First().Name)))
        .ToListAsync();

    public async Task<IEnumerable<BookShortInfo>> GetAllBooksShortInfoByAuthorIdAsync(int authorId) => await _bookContext.Books
        .AsNoTracking()
        .Include(b => b.Publisher)
        .Include(b => b.Authors)
        .Where(b => b.Authors.Any(a => a.Id == authorId))
        .Select(b => new BookShortInfo(b.Id, b.Name, new AuthorShortInfo(b.Authors!.First().Id, b.Authors!.First().Name)))
        .ToListAsync();

    public async Task<IEnumerable<BookShortInfo>> GetAllBooksShortInfoAsync() => await _bookContext.Books
        .AsNoTracking()
        .Include(b => b.Publisher)
        .Include(b => b.Authors)
        .Select(b => new BookShortInfo(b.Id, b.Name, new AuthorShortInfo(b.Authors!.First().Id, b.Authors!.First().Name)))
        .ToListAsync();

    public async Task<PagedList<BookShortInfo>> GetBooksShortInfoAsync(BookParameters parameters)
    {
        var result = await _bookContext.Books
            .AsNoTracking()
            .OrderBy(b => b.Name)
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .Include(b => b.Authors)
            .Include(b => b.Publisher)
            .Select(b => new BookShortInfo(b.Id, b.Name, new AuthorShortInfo(b.Authors!.First().Id, b.Authors!.First().Name)))
            .ToListAsync();

        return new PagedList<BookShortInfo>(result, parameters.PageSize, parameters.PageNumber, result.Count);
    }

    public async Task<int> CreateBookAsync(BookCreateDto dto)
    {
        var book = BookCreateDto.Convert(dto);

        return await SaveBook(dto, book);
    }
    public async Task<int> CreateBookWithCoverAsync(BookWithCoverCreateDto dto)
    {
        var book = BookWithCoverCreateDto.Convert(dto, CoverReader.ReadAllBytes(dto.Cover), CoverReader.GetFileName());

        return await SaveBook(dto.Details, book);
    }

    private async Task<int> SaveBook(BookCreateDto dto, Book book)
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


    public async Task<BookInfo?> GetBookAsync(int id)
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

    public async Task<bool> RemoveBookAsync(int id)
    {
        var rowsDeleted = await _bookContext.Books.Where(b => b.Id == id).ExecuteDeleteAsync();
        return rowsDeleted > 0;
    }

    public async Task<bool> UpdateBookAsync(int id, BookUpdateDto bookDto)
    {
        var book = _bookContext.Books.Include(b => b.BookGenres).FirstOrDefault(b => b.Id == id);
        if (book == null)
        {
            return false;
        }
        
        book.Name = bookDto.Name;
        
        if (book.BookGenres != null)
        {
            book.BookGenres.ToList().Clear();
        }
        
        book.BookGenres = bookDto.GenresId.Select(genreId => new BookGenre() { BookId = id, GenreId = genreId}).ToList();

        await _bookContext.SaveChangesAsync();

        return true;
    }

    public async Task<BookCoverDTO?> GetCoverAsync(int id)
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

    public async Task<bool> UpdateCoverAsync(int id, IFormFile file)
    {
        byte[] readen = CoverReader.ReadAllBytes(file);
        string newName = CoverReader.GetFileName();

        var rowsUpdated = await _bookContext.Set<BookCover>()
            .Where(c => c.BookId == id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(c => c.Name, newName)
                .SetProperty(c => c.CoverFile, readen)
                .SetProperty(c => c.BookId, id));

        if (rowsUpdated > 0)
        {
            return true;
        }

        // Trying to create a row, if a book with the Id is created but no entry was inserted in BookCover table
        var bookCover = new BookCover() { BookId = id,  CoverFile = readen, Name = newName  };
        _bookContext.Set<BookCover>().Add(bookCover);

        try
        {
            await _bookContext.SaveChangesAsync();
        }
        catch (DbUpdateException e) // when the book id is not present in Book table
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateBookRatingAsync(int id, int rating)
    {
        int rowsUpdated = await _bookContext.Set<BookRating>()
            .Where(r => r.BookId == id)
            .ExecuteUpdateAsync(parameters =>
                parameters.SetProperty(r => r.Rating, rating));

        if (rowsUpdated > 0)
        {
            return true;
        }
        
        // Trying to create a row, if a book with the Id is created but no entry was inserted in BookRating table
        var bookRating = new BookRating() { BookId = id,  Rating = rating };
        _bookContext.Set<BookRating>().Add(bookRating);
        
        try
        {
            await _bookContext.SaveChangesAsync();
        }
        catch (DbUpdateException e) // when the book id is not present in Book table
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateBookNameAsyns(int id, string newName)
    {
        int rowsUpdated  = await _bookContext.Books
            .Where(book => book.Id == id)
            .ExecuteUpdateAsync(parameters => 
                parameters.SetProperty(c => c.Name, newName));

        return rowsUpdated > 0;
    }

    public async Task<bool> UpdateBookAmountAsync(int id, int amount)
    {
        int rowsUpdated = await _bookContext.Set<BookAmount>()
            .Where(r => r.BookId == id)
            .ExecuteUpdateAsync(parameters =>
                parameters.SetProperty(r => r.Amount, amount));
        if (rowsUpdated > 0)
        {
            return true;
        }
        
        // Trying to create a row, if a book with the Id is created but no entry was inserted in BookAmount table
        var bookAmount = new BookAmount() { BookId = id,  Amount = amount};
        _bookContext.Set<BookAmount>().Add(bookAmount);
        
        try
        {
            await _bookContext.SaveChangesAsync();
        }
        catch (DbUpdateException e) // when the book id is not present in Book table
        {
            return false;
        }

        return true;
    }
}