using SC.API.CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.API
{
    public static class MigrationManager
    {
        public static async Task<IHost> MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope()) {
                var services = scope.ServiceProvider;

                try {
                    var context = services.GetRequiredService<ApplicationDbContext>();

                    if (context.Database.IsNpgsql()) {
                        await context.Database.MigrateAsync();
                    }
                }
                catch (Exception ex) {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                    logger.LogError(ex, "An error occurred while migrating or seeding the database.");

                    throw;
                }
            }

            return host;
        }
    }
}
