using LibraryManagementSystemAPI.Publisher.Data;

namespace LibraryManagementSystemAPI.Publisher;

public interface IPublisherRepository
{
    Task<PublisherFullInfo?> GetPublisher(int id);
    Task<PublisherFullInfo> CreatePublisher(PublisherInfo author);
    Task<bool> UpdatePublisher(int id, PublisherInfo info);
    Task<bool> DeletePublisher(int id);
    Task<IEnumerable<PublisherFullInfo>> GetAllPublishers();
}