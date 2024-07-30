using FluentValidation;
using LibraryManagementSystemAPI.Publisher;
using LibraryManagementSystemAPI.Publisher.Data;

namespace LibraryManagementSystemAPI.Controllers;

public class PublisherValidator : AbstractValidator<PublisherInfo>
{
    public PublisherValidator(IPublisherRepository publisherRepository)
    {
        RuleFor(p => p.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Length(3, 50)
            .Matches("^[a-zA-Z_]+( [a-zA-Z_]+)*$")
            .WithMessage("{PropertyName} must have only letters!")
            .MustAsync(async (name, _) =>
                await publisherRepository.IsPublisherUnique(name)
            ).WithMessage("{PropertyName} is not unique!");

        RuleFor(p => p.Address)
            .NotEmpty()
            .Length(5, 100);
    }
}