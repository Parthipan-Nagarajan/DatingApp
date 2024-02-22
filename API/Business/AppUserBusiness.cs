using System.Diagnostics;

namespace API;

public  static class AppUserBusiness
{
    public static void RegisterAppUserActions(this WebApplication application)
    {
        application.MapGet("/userCustom", () =>
        {
            return "Data";
        })
        .WithName("GetCustomBusinessLogic")
        .WithOpenApi();
    }
}
