using FluentValidation;
using LibraryManagementSystemAPI.Authors.Models;
using LibraryManagementSystemAPI.Models;
using LibraryManagementSystemAPI.Validators;
using Mediator;

namespace LibraryManagementSystemAPI.Authors.Commands;

internal sealed class UpdateAuthorHandler(IAuthorRepository authorRepository, IValidator<AuthorInfo> validator)
    : IRequestHandler<UpdateAuthorCommand, Error?>
{
    public async ValueTask<Error?> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request.Info, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return Error.BadRequest(validationResult.GetErrorMessages());
        }
        
        bool updated = await authorRepository.UpdateAuthorAsync(request.Id, request.Info);
        if (updated == false)
        {
            return Error.NotFound();
        }

        return null;
    }
}