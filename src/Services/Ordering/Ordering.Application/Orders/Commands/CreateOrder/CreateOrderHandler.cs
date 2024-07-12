using Ordering.Application.Data;
using Ordering.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler(IApplicationDBContext applicationDBContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var appedOrder = CreateOrder(command.Order);
            applicationDBContext.Orders.Add(appedOrder);
            await applicationDBContext.SaveChangesAsync(cancellationToken);
            return new CreateOrderResult (appedOrder.Id.Value );
        }

        private Order CreateOrder(OrderDto orderDto) 
        { 
            var billingAddress = Address.Of(orderDto.BillingAddress.FirstName, 
                orderDto.BillingAddress.LastName,
                orderDto.BillingAddress.EmailAddress,
                orderDto.BillingAddress.AddressLine,
                orderDto.BillingAddress.Country,
                orderDto.BillingAddress.State,
                orderDto.BillingAddress.ZipCode);
            var shippingAddress = Address.Of(orderDto.ShippingAddress.FirstName,
               orderDto.ShippingAddress.LastName,
               orderDto.ShippingAddress.EmailAddress,
               orderDto.ShippingAddress.AddressLine,
               orderDto.ShippingAddress.Country,
               orderDto.ShippingAddress.State,
               orderDto.ShippingAddress.ZipCode);
            var payment = Payment.Of(orderDto.Payment.CardNumber,
                orderDto.Payment.CardHolderName,
                orderDto.Payment.Expiration,
                orderDto.Payment.Cvv,
                orderDto.Payment.PaymentMethod);

            var newOrder = Order.Create(OrderId.Of(Guid.NewGuid()),
                CustomerId.Of(orderDto.CustomerId),
                OrderName.Of(orderDto.OrderName), 
                shippingAddress, 
                billingAddress, 
                payment);
            foreach(var item in orderDto.OrderItems)
            {
                newOrder.AddOrderItem(ProductId.Of(item.ProductId), item.Quantity, item.Price);
            }

            return newOrder;
        }

        
    }
}
