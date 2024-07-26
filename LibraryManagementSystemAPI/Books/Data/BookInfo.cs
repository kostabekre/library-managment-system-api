using LibraryManagementSystemAPI.Authors.Models;
using LibraryManagementSystemAPI.Models;

namespace LibraryManagementSystemAPI.Books.Data;

public class BookInfo
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ISBN { get; set; }
    public IEnumerable<Author>? Authors { get; set; }
    public Publisher.Publisher? Publisher { get; set; }
    public IEnumerable<Genre.Genre>? Genres { get; set; }
    public int BookRating { get; set; }
    public int BookAmount { get; set; }
    public DateTime DatePublished { get; set; }

    public BookInfo() {}

    public BookInfo(Book book)
    {
        Id = book.Id;
        Name = book.Name;
        ISBN = book.ISBN;
        Authors = book.Authors;
        Publisher = book.Publisher;
        Genres = book.Genres.ToList();
        DatePublished = book.DatePublished;
        BookRating = book.Rating?.Rating ?? 0;
        BookAmount = book.Amount?.Amount ?? 0;
    }
}