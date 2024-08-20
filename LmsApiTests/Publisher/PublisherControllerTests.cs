using LibraryManagementSystemAPI.Publisher;
using LibraryManagementSystemAPI.Publisher.Commands;

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

        var response = await _fixture.WithNoExist().SendGet(query);

        Assert.Null(response);
    }
    
    [Fact]
    public async Task GetPublisherExists()
    {
        var query = new GetPublisherQuery(1);

        var response = await _fixture.WithExist().SendGet(query);

        Assert.NotNull(response);
        Assert.Equal(1, response.Id);
    }

    [Fact]
    public async Task DeletePublisherExists()
    {
        var command = new DeletePublisherCommand(1 );

        var response = await _fixture.WithDeleteExists().SendDelete(command);

        Assert.Null(response);
    }

    [Fact]
    public async Task DeletePublisherNotExists()
    {
        var command = new DeletePublisherCommand(1 );

        var response = await _fixture.WithDeleteNoExist().SendDelete(command);

        Assert.NotNull(response);
    }
}