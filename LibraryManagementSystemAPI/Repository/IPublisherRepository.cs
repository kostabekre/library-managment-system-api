using LibraryManagementSystemAPI.Models;

namespace LibraryManagementSystemAPI.Repository;

public interface IPublisherRepository
{
    Task<Publisher?> GetPublisher(int id);
    Task CreatePublisher(Publisher author);
    Task<bool> UpdatePublisher(int id, Publisher publisher);
    Task<bool> DeletePublisher(int id);
    Task<IEnumerable<Publisher>> GetAllPublishers();
}