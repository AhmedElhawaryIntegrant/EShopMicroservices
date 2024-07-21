using MassTransit;
using BuildingBlocks.Messaging.Events;
using Ordering.Application.Orders.Commands.CreateOrder;
using Microsoft.Extensions.Logging;

namespace Ordering.Application.Orders.EventHandlers.Integration
{
    public class BascketCheckoutEventHandler(ISender sender,ILogger<BascketCheckoutEventHandler> logger) : IConsumer<BasketCheckoutEvent>
    {
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            logger.LogInformation("Integration event handled {IntegrationEvent}", context.Message.GetType().Name);
           var command = MapToOrderCommand(context.Message);
            await sender.Send(command);
        }

        private CreateOrderCommand MapToOrderCommand(BasketCheckoutEvent basketCheckoutEvent)
        {
           var addressDto = new AddressDto(basketCheckoutEvent.FirstName, basketCheckoutEvent.LastName, basketCheckoutEvent.EmailAddress, basketCheckoutEvent.AddressLine, basketCheckoutEvent.Country, basketCheckoutEvent.State, basketCheckoutEvent.ZipCode);
           var paymentDto = new PaymentDto(basketCheckoutEvent.CardNumber, basketCheckoutEvent.CardHolderName, basketCheckoutEvent.Expiration, basketCheckoutEvent.Cvv, basketCheckoutEvent.PaymentMethod);
            var orderId = new Guid();
            var orderDto = new OrderDto(
                Id: orderId,
                CustomerId: basketCheckoutEvent.CustomerId,
                OrderName: basketCheckoutEvent.UserName,
                ShippingAddress: addressDto,
                BillingAddress: addressDto,
                Payment: paymentDto,
                Status: OrderStatus.Pending,
                OrderItems:
                [
                    new OrderItemDto(orderId,new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"),2,500),
                    new OrderItemDto(orderId,new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"),1,400)

                ]
                );

            return new CreateOrderCommand(orderDto);
        }
    }
}
