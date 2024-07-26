namespace LibraryManagementSystemAPI.Books.Data;

public class BookAmount
{
    public int BookId { get; set; }
    public int Amount { get; set; }
    public Book Book { get; set; }
}