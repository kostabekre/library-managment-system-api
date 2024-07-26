using System.ComponentModel.DataAnnotations;
using LibraryManagementSystemAPI.Models;

namespace LibraryManagementSystemAPI.Books.Data;

public class BookCreateDto
{
    [MaxLength(100)] 
    public string Name { get; set; } = null!;
    [MaxLength(30)]
    public string Isbn { get; set; } = Guid.NewGuid().ToString();

    public int[] AuthorsId { get; set; } = null!;
    public int PublisherId { get; set; }
    public int[] GenresId { get; set; } = null!;
    public int BookAmount { get; set; }
    public int BookRating { get; set; }
    public DateTime DatePublished { get; set; }
    
    public static Book Convert(BookCreateDto dto)
    {
        var book = new Book()
        {
            Name = dto.Name,
            ISBN = dto.Isbn,
            PublisherId = dto.PublisherId,
            DatePublished = dto.DatePublished,
            Amount = new BookAmount(){Amount = dto.BookAmount},
            Rating = new BookRating(){Rating = dto.BookRating},
        };

        return book;
    }
}