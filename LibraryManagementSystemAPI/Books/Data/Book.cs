using LibraryManagementSystemAPI.Authors.Models;
using LibraryManagementSystemAPI.Models;

namespace LibraryManagementSystemAPI.Books.Data;

public class Book
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ISBN { get; set; } = Guid.NewGuid().ToString();
    public string? Description { get; set; }
    public BookCover? Cover { get; set; }
    public IEnumerable<BookAuthor>? BookAuthors { get; set; }
    public IEnumerable<Author> Authors { get; set; } = null!;
    public int PublisherId { get; set; }
    public Publisher.Data.Publisher Publisher { get; set; }
    public IEnumerable<BookGenre>? BookGenres { get; set; }
    public IEnumerable<Genre.Data.Genre>? Genres { get; set; }
    public BookRating? Rating { get; set; }
    public BookAmount? Amount { get; set; }
    public DateTime DatePublished { get; set; }
}