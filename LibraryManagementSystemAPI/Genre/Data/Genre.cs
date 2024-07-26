using LibraryManagementSystemAPI.Books.Data;

namespace LibraryManagementSystemAPI.Genre.Data;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<BookGenre>? BookGenres { get; set; }
    public IEnumerable<Book>? Books { get; set; }
}