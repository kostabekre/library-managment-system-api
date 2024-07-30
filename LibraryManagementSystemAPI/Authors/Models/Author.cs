using System.Text.Json.Serialization;
using LibraryManagementSystemAPI.Books.Data;

namespace LibraryManagementSystemAPI.Authors.Models;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Biography { get; set; }
    public IEnumerable<BookAuthor>? BookAuthors { get; set; }
    public IEnumerable<Book>? Books { get; set; }
}