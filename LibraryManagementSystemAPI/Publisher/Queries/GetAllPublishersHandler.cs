using LibraryManagementSystemAPI.Publisher.Data;
using Mediator;

namespace LibraryManagementSystemAPI.Publisher;

internal sealed class GetAllPublishersHandler(IPublisherRepository publisherRepository)
    : IRequestHandler<GetAllPublishersQuery, IEnumerable<PublisherFullInfo>>
{
    public async ValueTask<IEnumerable<PublisherFullInfo>> Handle(GetAllPublishersQuery request, CancellationToken cancellationToken)
    {
        return await publisherRepository.GetAllPublishersAsync();
    }
}