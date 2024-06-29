using Discount.grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.grpc.Data
{
    public class DiscountContext : DbContext
    {
        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {
        }

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(new Coupon { Id = 1, ProductName = "IPhone X", Description = "IPhone Discount", Amount = 150 });
            modelBuilder.Entity<Coupon>().HasData(new Coupon { Id = 2, ProductName = "Samsung 10", Description = "Samsung Discount", Amount = 100 });
            modelBuilder.Entity<Coupon>().HasData(new Coupon { Id = 3, ProductName = "Huawei P30", Description = "Huawei Discount", Amount = 50 });
        }
    }
    
}
