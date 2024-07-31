using FluentValidation;
using LibraryManagementSystemAPI.Models;
using LibraryManagementSystemAPI.Publisher.Data;
using Mediator;

namespace LibraryManagementSystemAPI.Publisher.Commands;

public class UpdatePublisherHandler : IRequestHandler<UpdatePublisherCommand, Error?>
{
    private readonly IValidator<PublisherInfo> _validator;
    private readonly IPublisherRepository _publisherRepository;

    public UpdatePublisherHandler(IPublisherRepository publisherRepository, IValidator<PublisherInfo> validator)
    {
        _publisherRepository = publisherRepository;
        _validator = validator;
    }

    public async ValueTask<Error?> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Info);
        if (validationResult.IsValid == false)
        {
            return new Error(401, validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }
        
        bool updated = await _publisherRepository.UpdatePublisherAsync(request.Id, request.Info);
        if (updated == false)
        {
            return new Error(404, new []{"Not Found"});
        }

        return null;
    }
}