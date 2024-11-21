namespace LibraryManagementSystemAPI.Books.Data;

public class BookWithCoverCreateDto
{
    public BookCreateDto Details { get; set; } = null!;
    public IFormFile? Cover { get; set; }

    public static Book Convert(BookWithCoverCreateDto dto, byte[]? cover, string? coverName)
    {
        var book = BookCreateDto.Convert(dto.Details);
        book.Cover = cover == null ? null : new BookCover { CoverFile = cover, Name = coverName! };

        return book;
    }
}