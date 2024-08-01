using Microsoft.Extensions.Options;

namespace LibraryManagementSystemAPI.Books.CoverValidation;

public static class CoverValidationInjection
{
    public static IServiceCollection AddCoverValidationValues(this IServiceCollection collection, WebApplicationBuilder builder )
    {
        collection.Configure<CoverValidationOptions>(
            builder.Configuration.GetSection(CoverValidationOptions.SectionName));

        collection.AddSingleton<IValidateOptions<CoverValidationOptions>, CoverValidationOptionsValidator>();
        
        return collection;
    }

    /// <summary>
    /// In the current moment (01.08.2024) there is no documented way to validate options on start for source generated code in .Net 8.
    /// The workaround is to create the options class and validate it for values.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication AddCoverValidationCheck(this WebApplication app)
    {
        var options = app.Services.GetRequiredService<IOptions<CoverValidationOptions>>();
        var validator = app.Services.GetRequiredService<IValidateOptions<CoverValidationOptions>>();

        validator.Validate(null, options.Value);
        
        return app;
    }
}