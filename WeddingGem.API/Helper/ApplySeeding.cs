using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeddingGem.Data.Context;
using WeddingGem.Data.Entites;
using WeddingGem.Repository;

namespace WeddingGem.API.Helper
{
    public class ApplySeeding
    {
        public static async Task SeedApply(WebApplication app)
        {
            var serv = app.Services.CreateScope().ServiceProvider;
            var Log = serv.GetRequiredService<ILogger<StoreContextSeed>>();
            var usermanager = serv.GetRequiredService<UserManager<AppUser>>();
            var roleManager = serv.GetRequiredService<RoleManager<IdentityRole>>();
            try
            {
                var context = serv.GetRequiredService<AppDbContext>();
                await context.Database.MigrateAsync();
                await StoreContextSeed.RolesSeed(roleManager, Log);
                await StoreContextSeed.PackageSeed(context, Log);
                await StoreContextSeed.AccountSeed(context,usermanager, Log);
                await StoreContextSeed.StoreSeed(context, Log,usermanager);
            }
            catch (Exception ex)
            {
                Log.LogError(ex.Message);
            }

        }
    }
}
