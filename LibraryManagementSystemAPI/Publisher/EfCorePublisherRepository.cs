using LibraryManagementSystemAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystemAPI.Publisher;

public class EfCorePublisherRepository : IPublisherRepository
{
    private readonly BookContext _bookContext;

    public EfCorePublisherRepository(BookContext bookContext)
    {
        _bookContext = bookContext;
    }

    public async Task<Publisher?> GetPublisher(int id)
    {
        var publisher = await _bookContext.Publishers
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
        return publisher;
    }

    public async Task CreatePublisher(Publisher publisher)
    {
        _bookContext.Publishers.Add(publisher);
        await _bookContext.SaveChangesAsync();
    }

    public async Task<bool> UpdatePublisher(int id, Publisher publisher)
    {
        var updatedRows = await _bookContext.Publishers
            .Where(p => p.Id == id)
            .ExecuteUpdateAsync(properties => properties
                .SetProperty(p => p.Address, publisher.Address)
                .SetProperty(p => p.Name, publisher.Name));
        return updatedRows > 0;
    }

    public async Task<bool> DeletePublisher(int id)
    {
        var deletedRows = await _bookContext.Publishers.Where(p => p.Id == id).ExecuteDeleteAsync();
        return deletedRows > 0;
    }

    public async Task<IEnumerable<Publisher>> GetAllPublishers()
    {
        return await _bookContext.Publishers
            .AsNoTracking()
            .ToListAsync();
    }
}