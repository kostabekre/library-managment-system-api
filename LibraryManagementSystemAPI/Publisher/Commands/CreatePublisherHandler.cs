using FluentValidation;
using LibraryManagementSystemAPI.Models;
using LibraryManagementSystemAPI.Publisher.Data;
using Mediator;

namespace LibraryManagementSystemAPI.Publisher.Commands;

internal sealed class CreatePublisherHandler : IRequestHandler<CreatePublisherCommand, Result<PublisherFullInfo>>
{
    private readonly IValidator<PublisherInfo> _validator;
    private readonly IPublisherRepository _publisherRepository;

    public CreatePublisherHandler(IValidator<PublisherInfo> validator, IPublisherRepository publisherRepository)
    {
        _validator = validator;
        _publisherRepository = publisherRepository;
    }

    public async ValueTask<Result<PublisherFullInfo>> Handle(CreatePublisherCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Info);
        if (validationResult.IsValid == false)
        {
            return new Error(401, validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }
        
        return  await _publisherRepository.CreatePublisherAsync(request.Info);
    }
}