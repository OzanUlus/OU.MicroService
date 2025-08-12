using Microsoft.Extensions.Caching.Distributed;
using OU.Microservice.Shared.Services;
using OU.MicroService.Basket.Api.Const;
using System.Text.Json;

namespace OU.MicroService.Basket.Api.Features.Basket
{
    public class BasketService(IIdentityService identityService, IDistributedCache distributedCache)
    {
        private string GetCacheKey() => string.Format(ConstBasket.BasketCacheKey, identityService.UserId);

        public Task<string?> GetBasketFromCache(CancellationToken cancellationToken)
        {
            return distributedCache.GetStringAsync(GetCacheKey(), token: cancellationToken);
        }

        public async Task CreateBasketCacheAsync(Data.Basket basket, CancellationToken cancellationToken)
        {
            var basketAsString = JsonSerializer.Serialize(basket);
            await distributedCache.SetStringAsync(GetCacheKey(), basketAsString, token: cancellationToken);
        }
    }
}
