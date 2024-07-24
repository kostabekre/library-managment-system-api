using System.ComponentModel.DataAnnotations;
using LibraryManagementSystemAPI.Books.Data;

namespace LibraryManagementSystemAPI.Models;

public class BookCreateDTO
{
    [MaxLength(100)]
    public string Name { get; set; }
    [MaxLength(30)]
    public string ISBN { get; set; } = Guid.NewGuid().ToString();
    public int[] AuthorsId { get; set; }
    public int PublisherId { get; set; }
    public int[] GenresId { get; set; }
    public DateTime DatePublished { get; set; }
    public IFormFile? Cover { get; set; }

    public static Book Convert(BookCreateDTO dto, byte[]? cover, string? coverName)
    {
        var book = new Book()
        {
            Name = dto.Name,
            Cover = cover == null ? null : new BookCover(){CoverFile = cover, Name = coverName!},
            ISBN = dto.ISBN,
            PublisherId = dto.PublisherId,
            DatePublished = dto.DatePublished
        };

        return book;
    }
}