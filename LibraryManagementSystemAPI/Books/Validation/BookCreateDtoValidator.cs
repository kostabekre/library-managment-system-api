using FluentValidation;
using LibraryManagementSystemAPI.Authors;
using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Genre;
using LibraryManagementSystemAPI.Publisher;

namespace LibraryManagementSystemAPI.Books.CoverValidation;

public class BookCreateDtoValidator : AbstractValidator<BookCreateDto>
{
    public BookCreateDtoValidator(IAuthorRepository authorRepository, 
        IGenreRepository genreRepository,
        IPublisherRepository publisherRepository)
    {
        RuleFor(b => b.Name)
            .NotEmpty().WithMessage("Name must not be empty!")
            .Length(3, 50).WithMessage("Name length is invalid! {TotalLength}");

        RuleFor(b => b.BookAmount)
            .GreaterThanOrEqualTo(0).WithMessage("Amount cannot be less than 0!");

        RuleFor(b => b.BookRating)
            .GreaterThanOrEqualTo(0).WithMessage("Rating cannot be less than 0!")
            .LessThanOrEqualTo(5).WithMessage("Rating cannot be more than 5!");

        RuleFor(b => b.AuthorsId)
            .Cascade(CascadeMode.Stop)
            .Must((ids) => ids.Distinct().Count() == ids.Length)
            .WithMessage("Authors must have no duplicates!")
            .MustAsync(async (ids, _) => await authorRepository.AreAuthorsExistAsync(ids))
            .WithMessage("Some or all of given authors do not exist");

        RuleFor(b => b.GenresId)
            .Cascade(CascadeMode.Stop)
            .Must((ids) => ids.Distinct().Count() == ids.Length)
            .WithMessage("Genres must have no duplicates!")
            .MustAsync(async (ids, _) => await genreRepository.AreGenresExistAsync(ids))
            .WithMessage("Some or all of given genres do not exist");
        
        RuleFor(b => b.PublisherId)
            .MustAsync(async (id, _) => await publisherRepository.IsPublisherExistsAsync(id))
            .WithMessage("Given publisher doesn't exist!");

        RuleFor(b => b.DatePublished)
            .LessThan(DateTime.Now).WithMessage("{PropertyName} cannot be in the future!")
            .Must(BeAValidDate).WithMessage("{PropertyName} cannot be default value!");

        RuleFor(b => b.Isbn)
            .NotEmpty()
            .Matches(@"^(?=(?:[^0-9]*[0-9]){10}(?:(?:[^0-9]*[0-9]){3})?$)[\d-]+$")
            .WithMessage("ISBN is not valid!");

        RuleFor(b => b.Isbn.Length)
            .LessThanOrEqualTo(17).WithMessage("ISBN length cannot be more than 17 characters long!");
    }

    private bool BeAValidDate(DateTime date) => !date.Equals(default);
}