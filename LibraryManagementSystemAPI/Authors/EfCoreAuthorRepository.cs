using LibraryManagementSystemAPI.Authors.Models;
using LibraryManagementSystemAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystemAPI.Authors;

public class EfCoreAuthorRepository : IAuthorRepository
{
    private readonly BookContext _bookContext;

    public EfCoreAuthorRepository(BookContext bookContext)
    {
        _bookContext = bookContext;
    }

    public async Task<AuthorFullInfo?> GetAuthorAsync(int id)
    {
        Author? author = await _bookContext.Authors
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);

        if (author == null)
        {
            return null;
        }
        
        return (AuthorFullInfo)author;
    }

    public async Task<AuthorFullInfo> CreateAuthorAsync(AuthorInfo info)
    {
        var author = (Author)info;
        _bookContext.Authors.Add(author);
        await _bookContext.SaveChangesAsync();
        return (AuthorFullInfo)author;
    }

    public async Task<bool> DeleteAuthorAsync(int id)
    {
        var rowsDeleted = await _bookContext.Authors.Where(a => a.Id == id).ExecuteDeleteAsync();
        return rowsDeleted > 0;
    }

    public async Task<bool> UpdateAuthorAsync(int id, AuthorInfo author)
    {
        var rowsUpdated = await _bookContext.Authors
            .Where(a => a.Id == id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(b => b.Name, author.Name)
                .SetProperty(b => b.Biography, author.Biography));

        return rowsUpdated > 0;
    }

    public async Task<IEnumerable<AuthorFullInfo>> GetAllAuthorsAsync()
    {
        return await _bookContext.Authors
            .AsNoTracking()
            .Select(a => (AuthorFullInfo)a)
            .ToListAsync();
    }

    public async Task<bool> IsNameUniqueAsync(string name) => await _bookContext
        .Authors.AnyAsync(a => a.Name == name) == false;

    public async Task<bool> AreAuthorsExistAsync(int[] ids)
    {
        var validIds = await _bookContext.Authors.Where(a => ids.Contains(a.Id)).Select(a => a.Id).ToListAsync();

        return validIds.Count == ids.Length;
    }
}