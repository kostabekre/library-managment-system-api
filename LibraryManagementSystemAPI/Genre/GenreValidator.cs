using FluentValidation;
using LibraryManagementSystemAPI.Genre.Data;

namespace LibraryManagementSystemAPI.Genre;

public class GenreValidator : AbstractValidator<GenreInfo>
{
    public GenreValidator(IGenreRepository repository)
    {
        RuleFor(g => g.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Length(2, 50)
            .MustAsync(async (name, _) => 
                await repository.IsNameUniqueAsync(name))
            .WithMessage("{PropertyName} is not unique!");
    }
}