using Basket.API.Models;

namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBaketResult>;

    public record GetBaketResult(ShoppingCart Cart);
    public class GetBasketHandler : IQueryHandler<GetBasketQuery, GetBaketResult>
    {
        public async Task<GetBaketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            return new GetBaketResult(new ShoppingCart(query.UserName));
        }
    }
}
