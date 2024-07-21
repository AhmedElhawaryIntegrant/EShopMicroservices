using MassTransit;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.EventHandlers.Domain
{
    public class OrderCreatedEventHandler (IPublishEndpoint publishEndPoint,ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain event handled {DomainEvent}", domainEvent.GetType().Name);
            var orderCreatedEvent = domainEvent.Order.ToOrderDto();

            await publishEndPoint.Publish(orderCreatedEvent, cancellationToken);
            
           // return Task.CompletedTask;
        }
    }
}
