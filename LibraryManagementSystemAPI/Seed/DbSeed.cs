using LibraryManagementSystemAPI.Authors.Models;
using LibraryManagementSystemAPI.Books.Data;
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
            _bookContext.Genres.Add(new Genre.Data.Genre() { Name = "Fantasy" });
            _bookContext.Genres.Add(new Genre.Data.Genre() { Name = "Horror" });
            _bookContext.Genres.Add(new Genre.Data.Genre() { Name = "Bugi vugi" });
        }

        if (!_bookContext.Publishers.Any())
        {
            _bookContext.Publishers.Add(new Publisher.Data.Publisher() { Address = "Bb8", Name = "Holly ground" });
            _bookContext.Publishers.Add(new Publisher.Data.Publisher() { Address = "Transilvania", Name = "Vampire streets" });
            _bookContext.Publishers.Add(new Publisher.Data.Publisher() { Address = "Cave", Name = "Hungry monkeys" });
            _bookContext.Publishers.Add(new Publisher.Data.Publisher() { Address = "London", Name = "London times" });
        }

        _bookContext.SaveChanges();
        
        if (!_bookContext.Books.Any())
        {
            _bookContext.Books.AddRange(
                new Book()
                {
                    BookAuthors = new[] { new BookAuthor() { AuthorId = 1 } },
                    Name = "The way of kings",
                    Amount = new BookAmount() { Amount = 1 },
                    BookGenres = new[] { new BookGenre() { GenreId = 1 } },
                    PublisherId = 1,
                    DatePublished = new DateTime(2002, 05, 07, 13, 0, 0),
                    ISBN = "0-8131-1111-0"
                },
                new Book()
                {
                    BookAuthors = new[] { new BookAuthor() { AuthorId = 2 } },
                    Name = "Bulward of broken Dreams",
                    Amount = new BookAmount() { Amount = 2 },
                    BookGenres = new[] { new BookGenre() { GenreId = 2 } },
                    PublisherId = 2,
                    DatePublished = new DateTime(2004, 8, 14, 17, 0, 0),
                    ISBN = "0-3549-8312-1"
                },
                new Book()
                {
                    BookAuthors = new[] { new BookAuthor() { AuthorId = 3 } },
                    Name = "Walk on the moon",
                    Amount = new BookAmount() { Amount = 100 },
                    BookGenres = new[] { new BookGenre() { GenreId = 3 } },
                    PublisherId = 3,
                    DatePublished = new DateTime(1985, 5, 27, 12, 0 ,0),
                    ISBN = "0-7870-9655-5"
                }
            );
        }

        _bookContext.SaveChanges();
    }
}