using FluentValidation;
using LibraryManagementSystemAPI.Books.Commands;

namespace LibraryManagementSystemAPI.Books.CoverValidation;

public class UpdateBookAmountCommandValidator : AbstractValidator<UpdateBookAmountCommand>
{
    public UpdateBookAmountCommandValidator()
    {
        RuleFor(c => c.Amount)
            .GreaterThanOrEqualTo(0);
    }
}