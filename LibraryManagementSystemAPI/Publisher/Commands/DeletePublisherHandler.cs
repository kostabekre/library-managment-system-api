using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Publisher.Commands;

internal sealed class DeletePublisherHandler(IPublisherRepository publisherRepository)
    : IRequestHandler<DeletePublisherCommand, Error?>
{
    public async ValueTask<Error?> Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
    {
        bool deleted = await publisherRepository.DeletePublisherAsync(request.Id);
        
        return deleted == false ? Error.NotFound() : null;
    }
}