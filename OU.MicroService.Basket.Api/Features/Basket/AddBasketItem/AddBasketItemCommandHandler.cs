using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using OU.Microservice.Shared;
using OU.MicroService.Basket.Api.Const;
using OU.MicroService.Basket.Api.Dtos;
using System.Text.Json;

namespace OU.MicroService.Basket.Api.Features.Basket.AddBasketItem
{
    public class AddBasketItemCommandHandler(IDistributedCache distributedCache) : IRequestHandler<AddBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
            Guid userId = Guid.NewGuid();
            var cacheKey = string.Format(ConstBasket.BasketCacheKey, userId);

            var basketAsString = await distributedCache.GetStringAsync(cacheKey, token: cancellationToken);


            BasketDto? currentBasket;

            var newBasketItem = new BasketItemDto(request.CourseId, request.CourseName, request.ImageUrl, request.CoursePrice, null);

            if (string.IsNullOrEmpty(basketAsString))
            {

                currentBasket = new BasketDto(userId, [newBasketItem]);

                await CreateCachAsync(currentBasket, cacheKey, cancellationToken);

                return ServiceResult.SuccessAsNoContent();

            }
            
                currentBasket = JsonSerializer.Deserialize<BasketDto>(basketAsString);

                var existingBasketItem = currentBasket.BasketItems.FirstOrDefault(x => x.CourseId == request.CourseId);

                if (existingBasketItem is not null)
                {
                    currentBasket.BasketItems.Remove(existingBasketItem);
                }
             
                    currentBasket.BasketItems.Add(newBasketItem);


            await CreateCachAsync(currentBasket,cacheKey,cancellationToken);

            return ServiceResult.SuccessAsNoContent();




        }
        private async Task CreateCachAsync(BasketDto basket, string cacheKey, CancellationToken cancellationToken) 
        {
           var basketAsString = JsonSerializer.Serialize(basket);

            await distributedCache.SetStringAsync(cacheKey, basketAsString, token: cancellationToken);
        }
    }
}
