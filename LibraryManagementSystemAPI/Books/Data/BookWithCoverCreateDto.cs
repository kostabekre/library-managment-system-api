namespace LibraryManagementSystemAPI.Books.Data;

public class BookWithCoverCreateDto
{
    public BookCreateDto Details { get; set; } = null!;
    public IFormFile? Cover { get; set; }

    public static Book Convert(BookWithCoverCreateDto dto, byte[]? cover, string? coverName)
    {
        var book = new Book()
        {
            Name = dto.Details.Name,
            Cover = cover == null ? null : new BookCover(){CoverFile = cover, Name = coverName!},
            ISBN = dto.Details.Isbn,
            PublisherId = dto.Details.PublisherId,
            DatePublished = dto.Details.DatePublished,
            Amount = new BookAmount(){Amount = dto.Details.BookAmount},
            Rating = new BookRating(){Rating = dto.Details.BookRating},
        };

        return book;
    }
}