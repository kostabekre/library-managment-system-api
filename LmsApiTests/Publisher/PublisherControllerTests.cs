using LibraryManagementSystemAPI.Publisher;

namespace LmsApiTests.Publisher;

public class PublisherControllerTests
{
    private PublisherFixture _fixture;

    public PublisherControllerTests()
    {
        _fixture = new PublisherFixture();
    }
    [Fact]
    public async Task GetPublisherNotExists()
    {
        var query = new GetPublisherQuery(1);

        var response = await _fixture.WithNoExist().Send(query);

        Assert.Null(response);
    }
    
    [Fact]
    public async Task GetPublisherExists()
    {
        var query = new GetPublisherQuery(1);

        var response = await _fixture.WithExist().Send(query);

        Assert.NotNull(response);
        Assert.Equal(1, response.Id);
    }
}