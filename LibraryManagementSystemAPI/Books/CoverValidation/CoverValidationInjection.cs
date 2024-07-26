namespace LibraryManagementSystemAPI.CoverValidation;

public static class CoverValidationInjection
{
    public static IServiceCollection AddCoverValidationValues(this IServiceCollection collection, WebApplicationBuilder builder )
    {
        collection.Configure<CoverValidationValues>(
            builder.Configuration.GetSection(nameof(CoverValidationValues)));
        
        return collection;
    }
}