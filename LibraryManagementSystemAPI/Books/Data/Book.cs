namespace LibraryManagementSystemAPI.Models;

public class Book
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ISBN { get; set; } = Guid.NewGuid().ToString();
    public string? CoverPath { get; set; }
    public IEnumerable<Author>? Authors { get; set; }
    public int PublisherId { get; set; }
    public Publisher? Publisher { get; set; }
    public IEnumerable<BookGenre>? BookGenres { get; set; }
    public BooksRating BooksRating { get; set; }
    public BookAmount BookAmount { get; set; }
    public DateTime DatePublished { get; set; }
}