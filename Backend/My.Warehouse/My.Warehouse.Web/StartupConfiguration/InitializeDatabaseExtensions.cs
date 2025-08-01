using Microsoft.EntityFrameworkCore;
using My.Warehouse.Dal.Contexts;

namespace My.Warehouse.Web.StartupConfiguration;

public static class InitializeDatabaseExtensions
{
    public static void InitializeDatabase(this IApplicationBuilder app)
    {
        using IServiceScope serviceScope = app
            .ApplicationServices.GetService<IServiceScopeFactory>()
            .CreateScope();

        MigrateDatabase<ApplicationDbContext>(serviceScope);
    }

    private static void MigrateDatabase<TDbContext>(IServiceScope serviceScope)
        where TDbContext : DbContext
    {
        using var context = serviceScope.ServiceProvider.GetRequiredService<TDbContext>();
        context.Database.Migrate();
    }
}
