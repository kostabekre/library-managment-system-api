using LibraryManagementSystemAPI.Publisher.Data;
using Mediator;

namespace LibraryManagementSystemAPI.Publisher;

internal sealed class GetAllPublishersHandler : IRequestHandler<GetAllPublishersQuery, IEnumerable<PublisherFullInfo>>
{
    private readonly IPublisherRepository _publisherRepository;

    public GetAllPublishersHandler(IPublisherRepository publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }

    public async ValueTask<IEnumerable<PublisherFullInfo>> Handle(GetAllPublishersQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<PublisherFullInfo> publishers = await _publisherRepository.GetAllPublishersAsync();
        return publishers;
    }
}