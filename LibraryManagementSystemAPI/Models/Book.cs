namespace LibraryManagementSystemAPI.Models;

public class Book
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ISBN { get; set; } = Guid.NewGuid().ToString();
    public IEnumerable<Author>? Authors { get; set; }
    public int PublisherId { get; set; }
    public Publisher? Publisher { get; set; }
    public IEnumerable<BookGenre>? BookGenres { get; set; }
    public DateTime DatePublished { get; set; }
}