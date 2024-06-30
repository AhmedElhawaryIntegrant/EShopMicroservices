using Microsoft.EntityFrameworkCore;

namespace Discount.grpc.Data
{
    public static class Extentions
    {
        public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<DiscountContext>();
            context.Database.MigrateAsync();
             context.Database.OpenConnectionAsync();
            context.Database.ExecuteSqlRawAsync("PRAGMA journal_mode=DELETE");
            context.Database.CloseConnectionAsync();
            return app;
        }
    }
}
