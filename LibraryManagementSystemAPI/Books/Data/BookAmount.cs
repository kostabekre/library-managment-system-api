using LibraryManagementSystemAPI.Books.Data;

namespace LibraryManagementSystemAPI.Models;

public class BookAmount
{
    public int BookId { get; set; }
    public int Amount { get; set; }
    public Book Book { get; set; }
}