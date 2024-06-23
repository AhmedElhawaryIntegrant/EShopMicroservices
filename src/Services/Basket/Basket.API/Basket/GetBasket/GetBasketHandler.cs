using Basket.API.Data;
using Basket.API.Models;

namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBaketResult>;

    public record GetBaketResult(ShoppingCart Cart);
    public class GetBasketHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBaketResult>
    {
        public async Task<GetBaketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            var basket = await repository.GetBasket(query.UserName);
            return new GetBaketResult(basket);
        }
    }
}
