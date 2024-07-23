using System.Text.Json.Serialization;
using LibraryManagementSystemAPI.Books.Data;

namespace LibraryManagementSystemAPI.Models;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }
    [JsonIgnore]
    public IEnumerable<BookGenre>? BookGenres { get; set; }
    [JsonIgnore]
    public IEnumerable<Book>? Books { get; set; }
}