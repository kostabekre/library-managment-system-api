using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystemAPI.Books.Data;

public class BookUpdateDto
{
    [MaxLength(100)] public string Name { get; init; } = null!;
    public int[] GenresId { get; init; } = null!;
    public string? Description { get; init; }
}