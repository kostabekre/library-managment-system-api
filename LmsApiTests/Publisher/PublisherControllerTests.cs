using LibraryManagementSystemAPI.Publisher;
using LibraryManagementSystemAPI.Publisher.Commands;
using LibraryManagementSystemAPI.Publisher.Data;

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
    
    [Fact]
    public async Task UpdatePublisherExists()
    {
        var command = new UpdatePublisherCommand(1, new PublisherInfo(){Address = "Fancy", Name = "Super New"} );

        var response = await _fixture.WithUpdateExists().SendUpdate(command);

        Assert.Null(response);
    }

    [Fact]
    public async Task UpdatePublisherNotExists()
    {
        var command = new UpdatePublisherCommand(1, new PublisherInfo(){Address = "Fancy", Name = "Super New"} );

        var response = await _fixture.WithUpdateNoExist().SendUpdate(command);

        Assert.NotNull(response);
    }
    
    [Fact]
    public async Task CreatePublisherNotExists()
    {
        var publisherInfo = new PublisherInfo() { Address = "Some Street", Name = "Lower horizon" };
        var command = new CreatePublisherCommand(publisherInfo);

        var response = await _fixture.WithCreateNoExist(publisherInfo).SendCreate(command);

        Assert.NotNull(response.Data);
        Assert.Equal(1, response.Data.Id);
        Assert.Equal(publisherInfo.Address, response.Data.Details.Address);
        Assert.Equal(publisherInfo.Name, response.Data.Details.Name);
    }
}