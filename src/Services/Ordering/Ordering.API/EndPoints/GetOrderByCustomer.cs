using Ordering.Application.Orders.Queries.GetOrderByCustomerQuery;
namespace Ordering.API.EndPoints
{
    public record GetOrderByResponse(IEnumerable<OrderDto> orders);
    public class GetOrderByCustomer : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
            {
                var query = new GetOrderByCustomerQuery(customerId);
                var result = await sender.Send(query);
                var response = result.Adapt<GetOrderByResponse>();
                return Results.Ok(response);
            }).WithName("GetOrderByCustomer")
            .Produces<GetOrderByResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Order By Customer")
            .WithDescription("Get Order By Customer");
        }
    }
    
}
