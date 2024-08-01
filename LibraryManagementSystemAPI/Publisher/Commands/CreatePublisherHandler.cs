using FluentValidation;
using LibraryManagementSystemAPI.Models;
using LibraryManagementSystemAPI.Publisher.Data;
using LibraryManagementSystemAPI.Validators;
using Mediator;

namespace LibraryManagementSystemAPI.Publisher.Commands;

internal sealed class CreatePublisherHandler(
    IValidator<PublisherInfo> validator,
    IPublisherRepository publisherRepository)
    : IRequestHandler<CreatePublisherCommand, Result<PublisherFullInfo>>
{
    public async ValueTask<Result<PublisherFullInfo>> Handle(CreatePublisherCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request.Info, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return Error.BadRequest(validationResult.GetErrorMessages());
        }
        
        return  await publisherRepository.CreatePublisherAsync(request.Info);
    }
}