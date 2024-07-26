namespace LibraryManagementSystemAPI.Genre.Data;

public class GenreInfo
{
    public string Name { get; init; }


    public static explicit operator GenreInfo(Genre genre) => Convert(genre);
    public static explicit operator Genre(GenreInfo genre) => Convert(genre);

    private static Genre Convert(GenreInfo info)
    {
        return new Genre() { Name = info.Name };
    }
    private static GenreInfo Convert(Genre genre)
    {
        return new GenreInfo() { Name = genre.Name };
    }
}