using FluentValidation;
using LibraryManagementSystemAPI.Books.Commands;

namespace LibraryManagementSystemAPI.Books.CoverValidation;

public class UpdateBookNameCommandValidator : AbstractValidator<UpdateBookNameCommand>
{
    public UpdateBookNameCommandValidator()
    {
        RuleFor(c => c.NewName)
            .NotEmpty().WithMessage("Name must not be empty!")
            .Length(3, 50).WithMessage("Name length is invalid! {TotalLength}");
    }
}