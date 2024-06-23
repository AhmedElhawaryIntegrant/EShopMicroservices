
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository repository,IDistributedCache distributedCach) : IBasketRepository
    {
        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
             await repository.DeleteBasket(userName, cancellationToken);
            await distributedCach.RemoveAsync(userName, cancellationToken);
            return true;
        }

        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await distributedCach.GetStringAsync(userName, cancellationToken);
            if(!string.IsNullOrEmpty(cachedBasket))
            {
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket);
            }
            else
            {
                var basket = await repository.GetBasket(userName, cancellationToken);
                await distributedCach.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);
                return basket;
            }
        
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            var baske =  await repository.StoreBasket(basket, cancellationToken);
            await distributedCach.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }
    }
}
