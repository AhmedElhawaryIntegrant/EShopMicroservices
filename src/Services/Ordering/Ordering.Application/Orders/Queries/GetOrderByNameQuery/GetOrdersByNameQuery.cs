using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Queries.GetOrderByNameQuery
{
    public record GetOrdersByNameResult(IEnumerable<OrderDto> Orders);
    public record class GetOrdersByNameQuery(string Name) : IQuery<GetOrdersByNameResult>;

}
