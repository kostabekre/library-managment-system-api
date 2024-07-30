using FluentValidation;
using LibraryManagementSystemAPI.Authors;
using LibraryManagementSystemAPI.Authors.Models;
using LibraryManagementSystemAPI.Books.CoverValidation;
using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Controllers;
using LibraryManagementSystemAPI.Genre;
using LibraryManagementSystemAPI.Genre.Data;
using LibraryManagementSystemAPI.Publisher.Data;

namespace LibraryManagementSystemAPI.Validators;

public static class ValidatorExtensions
{
    public static IServiceCollection AddValidators(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IValidator<PublisherInfo>, PublisherValidator>();
        serviceCollection.AddScoped<IValidator<GenreInfo>, GenreValidator>();
        serviceCollection.AddScoped<IValidator<AuthorInfo>, AuthorValidator>();
        serviceCollection.AddScoped<IValidator<CoverInfo>, BookCoverValidator>();
        serviceCollection.AddScoped<IValidator<BookCreateDto>, BookCreateDtoValidator>();
        return serviceCollection;
    }
}