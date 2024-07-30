namespace LibraryManagementSystemAPI.Authors.Models;

public class AuthorInfo
{
    public string Name { get; init; }
    public string? Biography { get; init; }

    public static explicit operator AuthorInfo(Author author) => Convert(author);
    public static explicit operator Author(AuthorInfo info) => Convert(info);

    public static AuthorInfo Convert(Author author)
    {
        return new AuthorInfo() { Name = author.Name, Biography = author.Biography };
    }
    public static Author Convert(AuthorInfo info)
    {
        return new Author() { Name = info.Name, Biography = info.Biography };
    }
}