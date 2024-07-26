namespace LibraryManagementSystemAPI.Publisher.Data;

public class PublisherInfo
{
    public string Name { get; init; } = null!;
    public string Address { get; init; } = null!;


    public PublisherInfo(Publisher publisher)
    {
        Name = publisher.Name;
        Address = publisher.Address;
    }
    public PublisherInfo()
    {
        
    }
    
    public static explicit operator PublisherInfo(Publisher publisher) => new PublisherInfo(publisher);
    
    public static explicit operator Publisher(PublisherInfo info) => Convert(info);

    private static Publisher Convert(PublisherInfo info)
    {
        return new Publisher() { Name = info.Name, Address = info.Name };
    }
}