using LibraryManagementSystemAPI.Authors.Models;
using LibraryManagementSystemAPI.Genre.Data;
using LibraryManagementSystemAPI.Publisher.Data;

namespace LibraryManagementSystemAPI.Books.Data;

public class BookInfo
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; init; }
    public string Isbn { get; set; } = null!;
    public IEnumerable<AuthorFullInfo> Authors { get; set; } = null!;
    public PublisherFullInfo? Publisher { get; set; }
    public IEnumerable<GenreFullInfo> Genres { get; set; } = null!;
    public int BookRating { get; set; }
    public int BookAmount { get; set; }
    public DateTime DatePublished { get; set; }

    public BookInfo() {}

    public BookInfo(Book book)
    {
        Id = book.Id;
        Name = book.Name;
        Description = book.Description;
        Isbn = book.ISBN;
        Authors = book.Authors!.Select(a => (AuthorFullInfo)a).ToList();
        Publisher = (PublisherFullInfo)book.Publisher!;
        Genres = book.Genres!.Select(g => (GenreFullInfo)g).ToList();
        DatePublished = book.DatePublished;
        BookRating = book.Rating?.Rating ?? 0;
        BookAmount = book.Amount?.Amount ?? 0;
    }
}