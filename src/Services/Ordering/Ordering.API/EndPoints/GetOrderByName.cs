using Ordering.Application.Orders.Queries.GetOrderByNameQuery;
namespace Ordering.API.EndPoints
{
    public record GetOrderByNameResponse(IEnumerable<OrderDto> orders);
    public class GetOrderByName : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/{orderName}", async (string orderName, ISender sender) =>
            {
                var query = new GetOrdersByNameQuery(orderName);
                var result = await sender.Send(query);
                var response = result.Adapt<GetOrderByNameResponse>();
                return Results.Ok(response);
            }).WithName("GetOrderByName")
            .Produces<GetOrderByNameResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Order By Name")
            .WithDescription("Get Order By Name");
        }
    }
   
}
