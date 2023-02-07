using Autentication.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Autentication.API.Configuration
{
    public static class DbMigrationHelpers
    {
        public static async Task MigrateAsync(IApplicationBuilder serviceScope)
        {
            var services = serviceScope.ApplicationServices.CreateScope().ServiceProvider;

            using var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AutenticationContext>();

            await context.Database.MigrateAsync();
        }
    }
}