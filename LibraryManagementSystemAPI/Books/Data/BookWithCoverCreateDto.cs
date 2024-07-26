namespace LibraryManagementSystemAPI.Books.Data;

public class BookWithCoverCreateDto
{
    public BookCreateDTO MainInfo { get; set; } 
    public IFormFile? Cover { get; set; }

    public static Book Convert(BookWithCoverCreateDto dto, byte[]? cover, string? coverName)
    {
        var book = new Book()
        {
            Name = dto.MainInfo.Name,
            Cover = cover == null ? null : new BookCover(){CoverFile = cover, Name = coverName!},
            ISBN = dto.MainInfo.ISBN,
            PublisherId = dto.MainInfo.PublisherId,
            DatePublished = dto.MainInfo.DatePublished,
            Amount = new BookAmount(){Amount = dto.MainInfo.BookAmount},
            Rating = new BookRating(){Rating = dto.MainInfo.BookRating},
        };

        return book;
    }
}