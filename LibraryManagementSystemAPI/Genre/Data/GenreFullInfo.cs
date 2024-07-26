namespace LibraryManagementSystemAPI.Genre.Data;

public class GenreFullInfo
{
    public int Id { get; init; }
    
    public GenreInfo Details { get; init; }

    public static explicit operator Genre(GenreFullInfo fullInfo) => Convert(fullInfo);
    public static explicit operator GenreFullInfo(Genre fullInfo) => Convert(fullInfo);
    
    private static GenreFullInfo Convert(Genre genre)
    {
        return new GenreFullInfo() { Id = genre.Id, Details = new GenreInfo(){Name = genre.Name} };
    }
    private static Genre Convert(GenreFullInfo fullInfo)
    {
        return new Genre() { Id = fullInfo.Id, Name = fullInfo.Details.Name };
    }
}