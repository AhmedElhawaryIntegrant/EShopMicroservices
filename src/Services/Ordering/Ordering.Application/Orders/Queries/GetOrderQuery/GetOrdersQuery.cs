

using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrderQuery
{
    public record class GetOrderResult(PaginatedResult<OrderDto> orders);
    public record GetOrdersQuery(PagintationRequest paginationRequest) : IQuery<GetOrderResult>;
   
}
