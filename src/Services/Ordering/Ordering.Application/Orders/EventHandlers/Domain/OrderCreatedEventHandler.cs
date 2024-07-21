using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.EventHandlers.Domain
{
    public class OrderCreatedEventHandler (IPublishEndpoint publishEndPoint,IFeatureManager featureManager,ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain event handled {DomainEvent}", domainEvent.GetType().Name);
            if(await featureManager.IsEnabledAsync("OrderFullfillment"))
            {
                var orderCreatedEvent = domainEvent.Order.ToOrderDto();

                await publishEndPoint.Publish(orderCreatedEvent, cancellationToken);
            }
           
            
           // return Task.CompletedTask;
        }
    }
}
