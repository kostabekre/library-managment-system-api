using FluentValidation;
using LibraryManagementSystemAPI.Authors.Models;
using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Authors.Commands;

internal sealed class UpdateAuthorHandler : IRequestHandler<UpdateAuthorCommand, Error?>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IValidator<AuthorInfo> _validator;

    public UpdateAuthorHandler(IAuthorRepository authorRepository, IValidator<AuthorInfo> validator)
    {
        _authorRepository = authorRepository;
        _validator = validator;
    }

    public async ValueTask<Error?> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Info);
        if (validationResult.IsValid == false)
        {
            return new Error(400, validationResult.Errors.Select(e => e.ErrorMessage));
        }
        
        bool updated = await _authorRepository.UpdateAuthorAsync(request.Id, request.Info);
        if (updated == false)
        {
            return new Error(401, new[] { "Not Found" });
        }

        return null;
    }
}