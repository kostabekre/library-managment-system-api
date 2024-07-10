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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BookGenre>()
            .HasKey(bg => new { bg.BookId, bg.GenreId });
    }
}