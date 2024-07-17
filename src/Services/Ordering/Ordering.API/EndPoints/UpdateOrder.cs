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
            }).WithName("UpdateOrder")
            .Produces<UpdateRecordResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Order")
            .WithDescription("Update Order");
        }
    }
}
