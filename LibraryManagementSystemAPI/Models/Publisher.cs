using System.Text.Json.Serialization;
using LibraryManagementSystemAPI.Books.Data;

namespace LibraryManagementSystemAPI.Models;

public class Publisher
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    [JsonIgnore]
    public IEnumerable<Book>? Books { get; set; }
}