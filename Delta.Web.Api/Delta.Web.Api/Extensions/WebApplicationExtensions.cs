using Microsoft.EntityFrameworkCore;
namespace Delta.Web.Api
{
    static class WebApplicationExtensions
    {
        public async static Task SeedDataAsync(this WebApplication app)
        {
            var scopeFactory = app.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory?.CreateScope();
            var services = scope?.ServiceProvider;
            try
            {
                var context = services?.GetRequiredService<DataContext>();
                var migration = context?.Database.MigrateAsync();
                if (migration != null) await migration;
                await Seed.SeedUsers(context);
            }
            catch (Exception e)
            {
                var logger = services?.GetRequiredService<ILogger<Program>>();
                logger?.LogError(e, "An error occured during migration");
            }
        }
        public static void ConfigureCors(this WebApplication app, IConfiguration configuration)
        {
            foreach (string host in (configuration.GetValue<string>("ServiceUrls") ?? string.Empty).Split(';'))
                app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().WithOrigins(host));
        }
    }
}
