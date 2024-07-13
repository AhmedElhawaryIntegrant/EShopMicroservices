using Ordering.Application.Data;
using Ordering.Application.Exceptions;
using Ordering.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandler(IApplicationDBContext context) :ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = OrderId.Of(command.Order.Id);
            var order = await context.Orders.FindAsync([orderId], cancellationToken);
            if(order == null)
            {
                throw new OrderNotFoundException(nameof(Order), orderId);
            }
            else
            {   UpdateOrder(order, command.Order);
                await context.SaveChangesAsync(cancellationToken);
                return new UpdateOrderResult(true);
            }
            throw new NotImplementedException();
        }

        private void UpdateOrder(Order order ,OrderDto orderDto)
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
           order.Update(OrderName.Of(orderDto.OrderName), shippingAddress, billingAddress, payment, orderDto.Status);

            
        }
    }
    
}
