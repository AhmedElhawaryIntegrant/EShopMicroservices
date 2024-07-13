


using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrderQuery
{
    public class GetOrdersHandler(IApplicationDBContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrderResult>
    {
        public async Task<GetOrderResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var pageSize = query.paginationRequest.PageSize;
            var pageIndex = query.paginationRequest.PageIndex;
            var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);
            var orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .OrderBy(o => o.OrderName)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
           return new GetOrderResult(new PaginatedResult<OrderDto>(pageIndex,pageSize,totalCount,orders.ToOrderDtoList()));
        }
    }
}
