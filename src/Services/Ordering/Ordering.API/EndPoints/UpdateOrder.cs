using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.EndPoints
{
    public record UpdateRecordRequest(OrderDto Order);

    public record UpdateRecordResponse(bool IsSuccess);
    public class UpdateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/orders", async (UpdateRecordRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateOrderCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateRecordResponse>();
                return Results.Ok(response);
            }).WithName("CreateOrder")
            .Produces<UpdateRecordResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Order")
            .WithDescription("Create Order");
        }
    }
}
