using FluentValidation;
using LibraryManagementSystemAPI.Controllers;
using LibraryManagementSystemAPI.Models;
using LibraryManagementSystemAPI.Publisher;
using LibraryManagementSystemAPI.Publisher.Commands;
using LibraryManagementSystemAPI.Publisher.Data;
using Mediator;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace LmsApiTests.Publisher;

public class PublisherFixture
{
    private Mock<IPublisherRepository> _mockRepository;
    
    public PublisherFixture WithCreateNoExist(PublisherInfo publisherInfo)
    {
        _mockRepository = new Mock<IPublisherRepository>();
        _mockRepository.Setup(repository => repository.CreatePublisherAsync(It.IsAny<PublisherInfo>()).Result)
            .Returns(new PublisherFullInfo()
                { Id = 1, Details = publisherInfo });
        _mockRepository.Setup(repository => repository.IsPublisherUniqueAsync(It.IsAny<string>()))
            .ReturnsAsync(true);
        
        return this;
    }
    
    public PublisherFixture WithUpdateExists()
    {
        _mockRepository = new Mock<IPublisherRepository>();
        _mockRepository.Setup(repository => repository.UpdatePublisherAsync(It.IsAny<int>(), It.IsAny<PublisherInfo>()))
            .ReturnsAsync(true);
        _mockRepository.Setup(repository => repository.IsPublisherUniqueAsync(It.IsAny<string>()))
            .ReturnsAsync(true);
        return this;
    }
    public PublisherFixture WithUpdateNoExist()
    {
        _mockRepository = new Mock<IPublisherRepository>();
        _mockRepository.Setup(repository => repository.UpdatePublisherAsync(1, new PublisherInfo(){Address = "Fancy", Name = "Super New"}).Result)
            .Returns(false);
        
        return this;
    }
    public PublisherFixture WithDeleteExists()
    {
        _mockRepository = new Mock<IPublisherRepository>();
        _mockRepository.Setup(repository => repository.DeletePublisherAsync(1).Result)
            .Returns(true);
        
        return this;
    }
    public PublisherFixture WithDeleteNoExist()
    {
        _mockRepository = new Mock<IPublisherRepository>();
        _mockRepository.Setup(repository => repository.DeletePublisherAsync(1).Result)
            .Returns(false);
        
        return this;
    }
    public PublisherFixture WithExist()
    {
        _mockRepository = new Mock<IPublisherRepository>();
        _mockRepository.Setup(repository => repository.GetPublisherAsync(1).Result)
            .Returns(new PublisherFullInfo()
                { Id = 1, Details = new PublisherInfo() { Address = "Argentine", Name = "Valida" } });
        
        return this;
    }

    
    public PublisherFixture WithNoExist()
    {
        _mockRepository = new Mock<IPublisherRepository>();
        
        return this;
    }

    
    public async Task<Result<PublisherFullInfo>> SendCreate(CreatePublisherCommand command)
    {
        var mediator = GetMediator();

        return await mediator.Send(command);
    }

    public async Task<Error?> SendUpdate(UpdatePublisherCommand command)
    {
        var mediator = GetMediator();

        return await mediator.Send(command);
    }
    public async Task<Error?> SendDelete(DeletePublisherCommand command)
    {
        var mediator = GetMediator();

        return await mediator.Send(command);
    }
    public async Task<PublisherFullInfo?> SendGet(GetPublisherQuery query)
    {
        var mediator = GetMediator();

        return await mediator.Send(query);
    }

    private IMediator GetMediator()
    {
        var services = new ServiceCollection();

        var serviceProvider = services
            .AddMediator(cfg =>
            {
                cfg.ServiceLifetime = ServiceLifetime.Transient;
            })
            .AddScoped(_ => _mockRepository.Object)
            .AddScoped(typeof(IValidator<PublisherInfo>), typeof(PublisherValidator))
            .BuildServiceProvider();

        var mediator = serviceProvider.GetRequiredService<IMediator>();
        return mediator;
    }
}