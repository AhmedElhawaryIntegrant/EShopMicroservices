


using Ordering.Application.Data;
using Ordering.Application.Exceptions;
using Ordering.Domain.ValueObject;

namespace Ordering.Application.Orders.Commands.DeleteOrder
{
    internal class DeleteOrderHandler(IApplicationDBContext dbContext) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
    {
        public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = OrderId.Of(command.OrderId);
            var order = await dbContext.Orders.FindAsync([orderId], cancellationToken);
            if(order == null)
            {
                throw new OrderNotFoundException(nameof(Order), orderId);
            }
            else
            {
                dbContext.Orders.Remove(order);
                await dbContext.SaveChangesAsync(cancellationToken);
                return new DeleteOrderResult(true);
            }
            
        }
    }
}
