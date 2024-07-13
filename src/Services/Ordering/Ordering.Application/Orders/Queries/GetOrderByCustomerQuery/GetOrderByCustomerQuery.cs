
namespace Ordering.Application.Orders.Queries.GetOrderByCustomerQuery
{
    public record GetOrderByCustomerResult(IEnumerable<OrderDto> Orders);
    public record GetOrderByCustomerQuery(Guid CustomerId) : IQuery<GetOrderByCustomerResult>;
    
}
