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
    public int BookAmount { get; set; }
    public int BookRating { get; set; }
    public DateTime DatePublished { get; set; }
    
    public static Book Convert(BookCreateDTO dto)
    {
        var book = new Book()
        {
            Name = dto.Name,
            ISBN = dto.ISBN,
            PublisherId = dto.PublisherId,
            DatePublished = dto.DatePublished,
            Amount = new BookAmount(){Amount = dto.BookAmount},
            Rating = new BookRating(){Rating = dto.BookRating},
        };

        return book;
    }
}