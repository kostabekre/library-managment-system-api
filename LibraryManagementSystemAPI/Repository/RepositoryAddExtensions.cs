using LibraryManagementSystemAPI.Controllers;

namespace LibraryManagementSystemAPI.Repository;

public static class RepositoryAddExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAuthorRepository, EfCoreAuthorRepository>();
        services.AddScoped<IBookRepository, EfCoreBookRepository>();
        services.AddScoped<IGenreRepository, EfCoreGenreRepository>();
        services.AddScoped<IPublisherRepository, IPublisherRepository>();
        return services;
    }
}