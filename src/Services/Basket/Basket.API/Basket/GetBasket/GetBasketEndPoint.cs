﻿using Carter;
using Mapster;

namespace Basket.API.Basket.GetBasket
{
   
    public record GetBasketResponse(ShoppingCart Cart);
    public class GetBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
            {
              
                var query = new GetBasketQuery(userName);
                var result = await sender.Send(query);
                var response = result.Adapt<GetBasketResponse>();
                return Results.Ok(response);
            }).WithName("Getbasket")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get basket")
            .WithDescription("Get basket"); ;
        }
    }
}
