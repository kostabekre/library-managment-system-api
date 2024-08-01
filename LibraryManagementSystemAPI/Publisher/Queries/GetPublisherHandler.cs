using LibraryManagementSystemAPI.Publisher.Data;
using Mediator;

namespace LibraryManagementSystemAPI.Publisher;

internal sealed class GetPublisherHandler(IPublisherRepository publisherRepository)
    : IRequestHandler<GetPublisherQuery, PublisherFullInfo?>
{
    public async ValueTask<PublisherFullInfo?> Handle(GetPublisherQuery request, CancellationToken cancellationToken)
    {
        return await publisherRepository.GetPublisherAsync(request.Id);
    }
}