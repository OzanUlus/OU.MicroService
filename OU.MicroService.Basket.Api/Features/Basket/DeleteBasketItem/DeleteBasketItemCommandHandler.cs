using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using OU.Microservice.Shared;
using OU.Microservice.Shared.Services;
using OU.MicroService.Basket.Api.Const;
using OU.MicroService.Basket.Api.Dtos;
using System.Net;
using System.Text.Json;

namespace OU.MicroService.Basket.Api.Features.Basket.DeleteBasketItem
{
    public class DeleteBasketItemCommandHandler(IDistributedCache distributedCache, IIdentityService identityService) : IRequestHandler<DeleteBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            var userId = identityService.GetUserId;
            var cacheKey = string.Format(ConstBasket.BasketCacheKey, userId);

            var basketAsString = await distributedCache.GetStringAsync(cacheKey, token:cancellationToken);

            if(string.IsNullOrEmpty(basketAsString))
            {
                return ServiceResult.Error("Basket not found", System.Net.HttpStatusCode.NotFound);
            }

            var currentBasket = JsonSerializer.Deserialize<BasketDto>(basketAsString);

            var basketItemToDelete = currentBasket!.BasketItems.FirstOrDefault(x => x.Id == request.Id);

            if (basketItemToDelete is null)
            {
                return ServiceResult.Error("Basket item not found", HttpStatusCode.NotFound);
            }

            currentBasket.BasketItems.Remove(basketItemToDelete);

            basketAsString = JsonSerializer.Serialize(currentBasket);

            await distributedCache.SetStringAsync(cacheKey, basketAsString, token:cancellationToken);


            return ServiceResult.SuccessAsNoContent();
        }
    }
}
