using LibraryManagementSystemAPI.Books.Data;

namespace LibraryManagementSystemAPI.Publisher.Data;

public class Publisher
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    
    public IEnumerable<Book>? Books { get; set; }
}