using LibraryManagementSystemAPI.Publisher.Data;
using Mediator;

namespace LibraryManagementSystemAPI.Publisher;

public class GetPublisherHandler : IRequestHandler<GetPublisherQuery, PublisherFullInfo?>
{
    private readonly IPublisherRepository _publisherRepository;

    public GetPublisherHandler(IPublisherRepository publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }

    public async ValueTask<PublisherFullInfo?> Handle(GetPublisherQuery request, CancellationToken cancellationToken)
    {
        PublisherFullInfo? publisher = await _publisherRepository.GetPublisherAsync(request.Id);
        return publisher;
    }
}