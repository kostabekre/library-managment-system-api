using System.Collections.Immutable;
using LibraryManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystemAPI.Context;

public class BookContext : DbContext
{
    public BookContext(DbContextOptions options) : base(options){ }
    
    public DbSet<Book> Books { get; init; }
    public DbSet<Author> Authors { get; init; }
    public DbSet<Genre> Genres { get; init; }
    public DbSet<Publisher> Publishers { get; init; }
    public DbSet<BooksRating> BooksRating { get; init; }
    public DbSet<BookAmount> BooksAmount { get; init; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BookGenre>()
            .HasKey(bg => new { bg.BookId, bg.GenreId });

        modelBuilder.Entity<BooksRating>()
            .HasKey(r => r.BookId);
        modelBuilder.Entity<BooksRating>()
            .HasOne(r => r.Book);

        modelBuilder.Entity<BookAmount>()
            .HasKey(a => a.BookId);
        modelBuilder.Entity<BookAmount>()
            .HasOne(a => a.Book);

    }
}