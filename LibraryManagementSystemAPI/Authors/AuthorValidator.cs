using FluentValidation;
using LibraryManagementSystemAPI.Authors.Models;

namespace LibraryManagementSystemAPI.Authors;

public class AuthorValidator : AbstractValidator<AuthorInfo>
{
    public AuthorValidator(IAuthorRepository authorRepository)
    {
        RuleFor(a => a.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Length(3, 50)
            .MustAsync(async (name, _) =>
                await authorRepository.IsNameUnique(name))
            .WithMessage("{PropertyName} is not unique!");
    }
}