using System.Text.Json.Serialization;
using LibraryManagementSystemAPI.Books.Data;

namespace LibraryManagementSystemAPI.Models;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Biography { get; set; }
    [JsonIgnore]
    public IEnumerable<BookAuthor>? BookAuthors { get; set; }
    [JsonIgnore]
    public IEnumerable<Book>? Books { get; set; }
}