using System.ComponentModel.DataAnnotations;

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

    public static Book Convert(BookCreateDTO dto, string? coverPath)
    {
        var book = new Book()
        {
            Name = dto.Name,
            ISBN = dto.ISBN,
            CoverPath = coverPath,
            PublisherId = dto.PublisherId,
            DatePublished = dto.DatePublished
        };

        return book;
    }
}