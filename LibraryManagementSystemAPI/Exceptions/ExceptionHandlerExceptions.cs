namespace LibraryManagementSystemAPI.Exceptions;

public static class ExceptionHandlerExceptions
{
    public static WebApplicationBuilder AddGlobalExceptionHandler(this WebApplicationBuilder builder)
    {
        if (builder.Environment.EnvironmentName.ToLower() != "development")
        {
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();
        }

        return builder;
    }
}