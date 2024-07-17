using BuildingBlocks.Pagination;
using Ordering.Application.Orders.Queries.GetOrderQuery;
namespace Ordering.API.EndPoints
{

    public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);
    public class GetOrders : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders", async ([AsParameters] PagintationRequest paginationRequest, ISender sender) =>
            {
                var query = new GetOrdersQuery(paginationRequest);
                var result = await sender.Send(query);
                var response = result.Adapt<GetOrdersResponse>();
                return Results.Ok(response);
            }).WithName("GetOrders")
            .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithDescription("Get Orders")
            .WithSummary("Get Orders");
        }
    }
}
