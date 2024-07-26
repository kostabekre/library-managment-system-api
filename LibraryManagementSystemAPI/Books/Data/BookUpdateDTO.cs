using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystemAPI.Books.Data;

public class BookUpdateDto
{
    [MaxLength(100)] public string Name { get; set; } = null!;
    public int[] GenresId { get; set; } = null!;
}