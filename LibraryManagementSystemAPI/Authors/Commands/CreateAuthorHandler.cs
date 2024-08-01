using FluentValidation;
using LibraryManagementSystemAPI.Authors.Models;
using LibraryManagementSystemAPI.Models;
using LibraryManagementSystemAPI.Validators;
using Mediator;

namespace LibraryManagementSystemAPI.Authors.Commands;

internal sealed class CreateAuthorHandler(IAuthorRepository authorRepository, IValidator<AuthorInfo> validator)
    : IRequestHandler<CreateAuthorCommand, Result<AuthorFullInfo>>
{
    public async ValueTask<Result<AuthorFullInfo>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request.Info, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return Error.BadRequest(validationResult.GetErrorMessages());
        }
        
        return await authorRepository.CreateAuthorAsync(request.Info);
    }
}