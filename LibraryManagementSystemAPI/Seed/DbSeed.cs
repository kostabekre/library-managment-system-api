using LibraryManagementSystemAPI.Authors.Models;
using LibraryManagementSystemAPI.Context;

namespace LibraryManagementSystemAPI.Seed;

public class DbSeed
{
    private readonly BookContext _bookContext;

    public DbSeed(BookContext bookContext)
    {
        _bookContext = bookContext;
    }

    public void Seed()
    {
        if (!_bookContext.Authors.Any())
        {
            _bookContext.Authors.Add(new Author() { Name = "Brandon Sanderson", Biography = "Fantasy writer" });
            _bookContext.Authors.Add(new Author() { Name = "Biloy", Biography = "Not fancy" });
            _bookContext.Authors.Add(new Author() { Name = "Charlsi", Biography = "Smart" });
            _bookContext.Authors.Add(new Author() { Name = "Jolt", Biography = "Not married" });
        }

        if (!_bookContext.Genres.Any())
        {
            _bookContext.Genres.Add(new Genre.Genre() { Name = "Fantasy" });
            _bookContext.Genres.Add(new Genre.Genre() { Name = "Horror" });
            _bookContext.Genres.Add(new Genre.Genre() { Name = "Bugi vugi" });
        }

        if (!_bookContext.Publishers.Any())
        {
            _bookContext.Publishers.Add(new Publisher.Publisher(){Address = "Bb8", Name = "Holly ground"});
            _bookContext.Publishers.Add(new Publisher.Publisher(){Address = "Transilvania", Name = "Vampire streets"});
            _bookContext.Publishers.Add(new Publisher.Publisher(){Address = "Cave", Name = "Hungry monkeys"});
            _bookContext.Publishers.Add(new Publisher.Publisher(){Address = "London", Name = "London times"});
        }

        _bookContext.SaveChanges();
    }
}