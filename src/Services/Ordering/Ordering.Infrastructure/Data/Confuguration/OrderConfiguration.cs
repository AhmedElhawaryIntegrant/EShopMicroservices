using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObject;
using Ordering.Domain.Enums;


namespace Ordering.Infrastructure.Data.Confuguration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(orderId => orderId.Value, dbId => OrderId.Of(dbId));
            builder.Property(x => x.CustomerId).IsRequired();
            
            builder.Property(x => x.TotalPrice).IsRequired();

            builder.HasOne<Customer>().WithMany().HasForeignKey(x => x.CustomerId);

            builder.HasMany<OrderItem>().WithOne().HasForeignKey(x => x.OrderId);

            builder.ComplexProperty(o => o.OrderName, modelBuilder => { 
            modelBuilder.Property(n=>n.Value).HasColumnName(nameof(Order.OrderName)).HasMaxLength(100).IsRequired();
            });

            builder.ComplexProperty(o => o.ShippingAddress, modelBuilder => { 
            
                modelBuilder.Property(f=>f.FirstName).HasMaxLength(50).IsRequired();
                modelBuilder.Property(l=>l.LastName).HasMaxLength(50).IsRequired();
                modelBuilder.Property(a=>a.EmailAddress).HasMaxLength(255);
                modelBuilder.Property(a=>a.AddressLine).HasMaxLength(180).IsRequired();
                modelBuilder.Property(a=>a.Country).HasMaxLength(50);
                modelBuilder.Property(a=>a.State).HasMaxLength(50);
                modelBuilder.Property(a=>a.ZipCode).HasMaxLength(5);
            
            });

            builder.ComplexProperty(o => o.BillingAddress, modelBuilder => {

                modelBuilder.Property(f => f.FirstName).HasMaxLength(50).IsRequired();
                modelBuilder.Property(l => l.LastName).HasMaxLength(50).IsRequired();
                modelBuilder.Property(a => a.EmailAddress).HasMaxLength(255);
                modelBuilder.Property(a => a.AddressLine).HasMaxLength(180).IsRequired();
                modelBuilder.Property(a => a.Country).HasMaxLength(50);
                modelBuilder.Property(a => a.State).HasMaxLength(50);
                modelBuilder.Property(a => a.ZipCode).HasMaxLength(5);

            });

            builder.ComplexProperty(o => o.Payment, modelBuilder => {

                modelBuilder.Property(f => f.CardHolderName).HasMaxLength(50);
                modelBuilder.Property(l => l.CardNumber).HasMaxLength(24).IsRequired();
                modelBuilder.Property(a => a.Expiration).HasMaxLength(10);
                modelBuilder.Property(a => a.CVV).HasMaxLength(3);
                modelBuilder.Property(a => a.PaymentMethod);
                

            });


            builder.Property(x => x.Status).HasDefaultValue(OrderStatus.Pending).HasConversion(status=> status.ToString(),
                dbStatus=> (OrderStatus)Enum.Parse(typeof(OrderStatus),dbStatus));
        }
    }
    }

