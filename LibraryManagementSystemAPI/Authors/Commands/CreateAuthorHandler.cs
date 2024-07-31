using FluentValidation;
using LibraryManagementSystemAPI.Authors.Models;
using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Authors.Commands;

internal sealed class CreateAuthorHandler : IRequestHandler<CreateAuthorCommand, Result<AuthorFullInfo>>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IValidator<AuthorInfo> _validator;

    public CreateAuthorHandler(IAuthorRepository authorRepository, IValidator<AuthorInfo> validator)
    {
        _authorRepository = authorRepository;
        _validator = validator;
    }

    public async ValueTask<Result<AuthorFullInfo>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Info);
        if (validationResult.IsValid == false)
        {
            new Error(400, validationResult.Errors.Select(e => e.ErrorMessage));
        }
        
        return await _authorRepository.CreateAuthorAsync(request.Info);
    }
}