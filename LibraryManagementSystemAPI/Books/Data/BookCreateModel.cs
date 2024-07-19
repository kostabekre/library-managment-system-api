namespace LibraryManagementSystemAPI.Books.Data;

public class BookCreateModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? CoverPath { get; set; }
    public int[] AuthorsId { get; set; }
    public int PublisherId { get; set; }
    public int[] BookGenresId { get; set; }
    public int BooksRating { get; set; }
    public int BookAmount { get; set; }
    public DateTime DatePublished { get; set; }
}