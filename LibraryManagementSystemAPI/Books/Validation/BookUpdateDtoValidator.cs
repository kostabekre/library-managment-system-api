using FluentValidation;
using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Genre;

namespace LibraryManagementSystemAPI.Books.CoverValidation;

public class BookUpdateDtoValidator : AbstractValidator<BookUpdateDto>
{
    public BookUpdateDtoValidator(IGenreRepository genreRepository)
    {
        RuleFor(b => b.Name)
            .NotEmpty().WithMessage("Name must not be empty!")
            .Length(3, 50).WithMessage("Name length is invalid! {TotalLength}");
        
        RuleFor(b => b.GenresId)
            .Cascade(CascadeMode.Stop)
            .Must((ids) => ids.Distinct().Count() == ids.Length)
            .WithMessage("Genres must have no duplicates!")
            .MustAsync(async (ids, _) => await genreRepository.AreGenresExistAsync(ids))
            .WithMessage("Some or all of given genres do not exist");
    }
}