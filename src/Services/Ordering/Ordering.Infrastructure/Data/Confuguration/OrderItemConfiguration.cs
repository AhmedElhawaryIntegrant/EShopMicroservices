using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObject;
using System.Security.Cryptography.X509Certificates;


namespace Ordering.Infrastructure.Data.Confuguration
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
                builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(orderItemId => orderItemId.Value, dbId => OrderItemId.Of(dbId));
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            
            builder.HasOne<Product>().WithMany().HasForeignKey(x => x.ProductId);
        }
    }
}
