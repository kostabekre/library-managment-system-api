namespace LibraryManagementSystemAPI.Publisher.Data;

public class PublisherFullInfo
{
    public int Id { get; init; }
    public PublisherInfo Details { get; init; }
    
    public static explicit operator Publisher(PublisherFullInfo info) => Convert(info);
    public static explicit operator PublisherFullInfo(Publisher publisher) => Convert(publisher);

    private static PublisherFullInfo Convert(Publisher publisher)
    {
        return new PublisherFullInfo() { Id = publisher.Id, Details = new PublisherInfo(publisher)};
    }
    private static Publisher Convert(PublisherFullInfo fullInfo)
    {
        return new Publisher() { Id = fullInfo.Id, Name = fullInfo.Details.Name, Address = fullInfo.Details.Name };
    }
}