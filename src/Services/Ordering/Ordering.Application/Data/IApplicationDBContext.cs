using Microsoft.EntityFrameworkCore;

namespace Ordering.Application.Data
{
    public interface IApplicationDBContext
    {

        DbSet<Customer> Customers { get; }

        DbSet<Order> Orders { get; }

        DbSet<OrderItem> OrderItems  { get; }

         DbSet<Product> Products { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
