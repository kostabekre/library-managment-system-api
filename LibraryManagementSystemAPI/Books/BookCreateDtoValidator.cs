using FluentValidation;
using LibraryManagementSystemAPI.Books.Data;

namespace LibraryManagementSystemAPI.Books.CoverValidation;

public class BookCreateDtoValidator : AbstractValidator<BookCreateDto>
{
    public BookCreateDtoValidator()
    {
        RuleFor(b => b.Name)
            .NotEmpty().WithMessage("Name must not be empty!")
            .Length(3, 50).WithMessage("Name length is invalid! {TotalLength}");

        RuleFor(b => b.BookAmount)
            .GreaterThanOrEqualTo(0).WithMessage("Amount cannot be less than 0!");

        RuleFor(b => b.BookRating)
            .GreaterThanOrEqualTo(0).WithMessage("Rating cannot be less than 0!")
            .LessThanOrEqualTo(10).WithMessage("Rating cannot be more than 10!");

        RuleFor(b => b.DatePublished)
            .LessThan(DateTime.Now).WithMessage("{PropertyName} cannot be in the future!")
            .Must(BeAValidDate).WithMessage("{PropertyName} cannot be default value!");

        RuleFor(b => b.Isbn)
            .NotEmpty()
            .Matches("ISBN(-1(?:(0)|3))?:?\\x20(\\s)*[0-9]+[- ][0-9]+[- ][0-9]+[- ][0-9]*[- ]*[xX0-9]")
            .WithMessage("ISBN is not valid!");

        RuleFor(b => b.Isbn.Length)
            .LessThanOrEqualTo(17).WithMessage("ISBN length cannot be more than 17 characters long!");
    }

    private bool BeAValidDate(DateTime date) => !date.Equals(default);
}