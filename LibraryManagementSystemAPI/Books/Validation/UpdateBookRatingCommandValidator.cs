using FluentValidation;
using LibraryManagementSystemAPI.Books.Commands;

namespace LibraryManagementSystemAPI.Books.CoverValidation;

public class UpdateBookRatingCommandValidator : AbstractValidator<UpdateBookRatingCommand>
{
    public UpdateBookRatingCommandValidator()
    {
        RuleFor(c => c.Rating)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(10);
    }
}