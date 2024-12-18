using LibraryManagementSystemAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystemAPI.Seed;

public static class DbSeedExtensions
{
    public static void AddSeedDb(this IServiceCollection collection)
    {
        collection.AddScoped<DbSeed>();
    }
    public static WebApplication UseSeedDb(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<BookContext>();
            context.Database.Migrate();
            var init = scope.ServiceProvider.GetRequiredService<DbSeed>();
            init.Seed();
        }

        return app;
    }
}