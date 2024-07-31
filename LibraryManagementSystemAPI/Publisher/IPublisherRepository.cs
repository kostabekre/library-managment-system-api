using LibraryManagementSystemAPI.Publisher.Data;

namespace LibraryManagementSystemAPI.Publisher;

public interface IPublisherRepository
{
    Task<PublisherFullInfo?> GetPublisherAsync(int id);
    Task<PublisherFullInfo> CreatePublisherAsync(PublisherInfo author);
    Task<bool> UpdatePublisherAsync(int id, PublisherInfo info);
    Task<bool> DeletePublisherAsync(int id);
    Task<IEnumerable<PublisherFullInfo>> GetAllPublishersAsync();
    Task<bool> IsPublisherUniqueAsync(string name);
}