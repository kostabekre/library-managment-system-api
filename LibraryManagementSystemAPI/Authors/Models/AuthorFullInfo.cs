namespace LibraryManagementSystemAPI.Authors.Models;

public class AuthorFullInfo
{
    public int Id { get; init; }
    public AuthorInfo Details { get; init; }
    
    public static explicit operator AuthorFullInfo(Author author) => Convert(author);
    public static explicit operator Author(AuthorFullInfo info) => Convert(info);

    public static AuthorFullInfo Convert(Author author)
    {
        return new AuthorFullInfo() { Id = author.Id, Details = AuthorInfo.Convert(author) };
    }
    public static Author Convert(AuthorFullInfo info)
    {
        return new Author() { Id = info.Id, Name = info.Details.Name, Biography = info.Details.Biography };
    }
}