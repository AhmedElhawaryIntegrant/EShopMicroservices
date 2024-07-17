using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Queries.GetOrderByCustomerQuery
{
    public class GetOrderByCustomerHandler(IApplicationDBContext dbContext) : IQueryHandler<GetOrderByCustomerQuery, GetOrderByCustomerResult>
    {
        public async Task<GetOrderByCustomerResult> Handle(GetOrderByCustomerQuery query, CancellationToken cancellationToken)
        {
            var orders= await dbContext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(o => o.CustomerId.Value == query.CustomerId)
                .OrderBy(o => o.OrderName.Value)
                .ToListAsync(cancellationToken);
            var orderDtos = orders.ToOrderDtoList();
            return new GetOrderByCustomerResult(orderDtos);
        }
    }
}
