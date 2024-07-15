
using Ordering.Application.Orders.Commands.CreateOrder;
namespace Ordering.API.EndPoints
{
    public record CreateRecordRequest(OrderDto Order);

    public record CreateRecordResponse(Guid Id);
    public class CreateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/orders", async (CreateRecordRequest request,ISender sender) =>
            {
               var command = request.Adapt<CreateOrderCommand>();
              var result = await sender.Send(command);
              var response = result.Adapt<CreateRecordResponse>();
                return Results.Created($"/orders/{response.Id}", response);
            }).WithName("CreateOrder")
            .Produces<CreateRecordResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Order")
            .WithDescription("Create Order");
        }
    }
}
