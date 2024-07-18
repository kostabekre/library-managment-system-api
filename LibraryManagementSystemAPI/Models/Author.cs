using System.Text.Json.Serialization;

namespace LibraryManagementSystemAPI.Models;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Biography { get; set; }
    [JsonIgnore]
    public IEnumerable<Book>? Books { get; set; }
}