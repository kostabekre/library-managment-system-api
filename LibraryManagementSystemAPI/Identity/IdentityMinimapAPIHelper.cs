using Microsoft.AspNetCore.Identity;

namespace LibraryManagementSystemAPI.Identity;

public static class IdentityMinimapApiHelper
{
    public static void MapLogout(this WebApplication app)
    {
        app.MapPost("/logout", async (SignInManager<IdentityUser> signInManager) =>
       {
            await signInManager.SignOutAsync();
            return Results.Ok();
        });
    }
}