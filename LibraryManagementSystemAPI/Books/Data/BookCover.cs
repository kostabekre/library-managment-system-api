using LibraryManagementSystemAPI.Models;

namespace LibraryManagementSystemAPI.Books.Data;

public class BookCover
{
    public Book Book { get; init; } = null!;

    public string Name { get; init; } = null!;
    public int BookId { get; init; }
    public byte[] CoverFile { get; init; } = null!;
}