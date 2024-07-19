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
    public DbSet<BookRating> BooksRating { get; init; }
    public DbSet<BookAmount> BooksAmount { get; init; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>()
            .HasMany(b => b.Genres)
            .WithMany(g => g.Books);
        
        modelBuilder.Entity<Book>()
            .HasMany(b => b.Authors)
            .WithMany(g => g.Books);

        modelBuilder.Entity<BookRating>()
            .HasOne(r => r.Book);
        modelBuilder.Entity<BookRating>()
            .HasKey(r => r.BookId);

        modelBuilder.Entity<BookAmount>()
            .HasOne(a => a.Book);
        modelBuilder.Entity<BookAmount>()
            .HasKey(a => a.BookId);

    }
}