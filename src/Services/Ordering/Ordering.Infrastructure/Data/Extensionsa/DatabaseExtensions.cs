using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Data.Extensions;

namespace Ordering.Infrastructure.Data.Extensionsa
{
    public static class DatabaseExtensions
    {
        public static async Task InitializeDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AbblicationDbContext>();
            context.Database.MigrateAsync().GetAwaiter().GetResult();
            await SeedDataAsync(context);

        }

        private static async Task SeedDataAsync(AbblicationDbContext context)
        {
            await SeedCustomerAsync(context);
            await SeedProductAsync(context);
            await SeedOrdersAsync(context);
        }
        private static async Task SeedCustomerAsync(AbblicationDbContext context)
        {
            if (!context.Customers.Any())
            {
                await context.Customers.AddRangeAsync(InitialData.Customers);
          
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedProductAsync(AbblicationDbContext context)
        {
            if (!context.Products.Any())
            {
                await context.Products.AddRangeAsync(InitialData.Products);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedOrdersAsync(AbblicationDbContext context)
        {
            if (!context.Orders.Any())
            {
                await context.Orders.AddRangeAsync(InitialData.OrdersWithItems);
                await context.SaveChangesAsync();
            }
        }
    }
}
