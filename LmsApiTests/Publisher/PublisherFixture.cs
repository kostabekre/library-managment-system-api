using LibraryManagementSystemAPI.Publisher;
using LibraryManagementSystemAPI.Publisher.Data;
using Mediator;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace LmsApiTests.Publisher;

public class PublisherFixture
{
    private Mock<IPublisherRepository> _repository;
    
    public PublisherFixture WithExist()
    {
        _repository = new Mock<IPublisherRepository>();
        _repository.Setup(repository => repository.GetPublisherAsync(1).Result)
            .Returns(new PublisherFullInfo()
                { Id = 1, Details = new PublisherInfo() { Address = "Argentine", Name = "Valida" } });
        
        return this;
    }

    public PublisherFixture WithNoExist()
    {
        _repository = new Mock<IPublisherRepository>();
        // _repository.Setup(repository => repository.GetPublisherAsync(1).Result)
        //     .Returns(new PublisherFullInfo());
        
        return this;
    }
    public async Task<PublisherFullInfo?> Send(GetPublisherQuery query)
    {
        var services = new ServiceCollection();

        var serviceProvider = services
            .AddMediator(cfg =>
            {
                cfg.ServiceLifetime = ServiceLifetime.Transient;
            })
            .AddScoped(_ => _repository.Object)
            .BuildServiceProvider();

        var mediator = serviceProvider.GetRequiredService<IMediator>();

        return await mediator.Send(query);
    }
}