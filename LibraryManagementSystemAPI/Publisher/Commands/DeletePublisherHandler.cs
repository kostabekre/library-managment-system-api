using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Publisher.Commands;

public class DeletePublisherHandler : IRequestHandler<DeletePublisherCommand, Error?>
{
    private readonly IPublisherRepository _publisherRepository;

    public DeletePublisherHandler(IPublisherRepository publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }

    public async ValueTask<Error?> Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
    {
        bool deleted = await _publisherRepository.DeletePublisherAsync(request.Id);
        if (deleted == false)
        {
            return new Error(404, new []{"Not Found"});
        }

        return null;
    }
}