using LibraryManagementSystemAPI.Context;
using LibraryManagementSystemAPI.Controllers;
using LibraryManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystemAPI.Repository;

public class EfCoreAuthorRepository : IAuthorRepository
{
    private readonly BookContext _bookContext;

    public EfCoreAuthorRepository(BookContext bookContext)
    {
        _bookContext = bookContext;
    }

    public async Task<Author?> GetAuthor(int id)
    {
        return await _bookContext.Authors.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Author> CreateAuthor(Author author)
    {
        _bookContext.Authors.Add(author);
        await _bookContext.SaveChangesAsync();
        return author;
    }

    public async Task<bool> DeleteAuthor(int id)
    {
        var rowsDeleted = await _bookContext.Authors.Where(a => a.Id == id).ExecuteDeleteAsync();
        return rowsDeleted > 0;
    }

    public async Task<bool> UpdateAuthor(int id, Author author)
    {
        var rowsUpdated = await _bookContext.Authors
            .Where(a => a.Id == id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(b => b.Name, author.Name)
                .SetProperty(b => b.Biography, author.Biography));

        return rowsUpdated > 0;
    }
}