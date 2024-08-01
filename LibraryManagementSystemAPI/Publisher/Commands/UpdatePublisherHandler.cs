using FluentValidation;
using LibraryManagementSystemAPI.Models;
using LibraryManagementSystemAPI.Publisher.Data;
using LibraryManagementSystemAPI.Validators;
using Mediator;

namespace LibraryManagementSystemAPI.Publisher.Commands;

internal sealed class UpdatePublisherHandler(
    IPublisherRepository publisherRepository,
    IValidator<PublisherInfo> validator)
    : IRequestHandler<UpdatePublisherCommand, Error?>
{
    public async ValueTask<Error?> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request.Info, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return Error.BadRequest(validationResult.GetErrorMessages());
        }
        
        bool updated = await publisherRepository.UpdatePublisherAsync(request.Id, request.Info);
        
        return updated == false ? Error.NotFound() : null;
    }
}