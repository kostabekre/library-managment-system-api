using LibraryManagementSystemAPI.Models;

namespace LibraryManagementSystemAPI.Books.Data;

public class BookInfo
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ISBN { get; set; }
    public string? CoverPath { get; set; }
    public IEnumerable<Author>? Authors { get; set; }
    public Publisher? Publisher { get; set; }
    public IEnumerable<Genre>? Genres { get; set; }
    public int BooksRating { get; set; }
    public int BookAmount { get; set; }
    public DateTime DatePublished { get; set; }

    public BookInfo() {}

    public BookInfo(Book book)
    {
        Id = book.Id;
        Name = book.Name;
        ISBN = book.ISBN;
        CoverPath = book.CoverPath;
        Authors = book.Authors;
        Publisher = book.Publisher;
        Genres = book.Genres.ToList();
        DatePublished = book.DatePublished;
    }
}