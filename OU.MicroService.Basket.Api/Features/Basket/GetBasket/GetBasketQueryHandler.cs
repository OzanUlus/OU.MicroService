using Mapster;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using OU.Microservice.Shared;
using OU.Microservice.Shared.Services;
using OU.MicroService.Basket.Api.Const;
using OU.MicroService.Basket.Api.Dtos;
using System.Net;
using System.Text.Json;

namespace OU.MicroService.Basket.Api.Features.Basket.GetBasket
{
    public class GetBasketQueryHandler(IDistributedCache distributedCache, IIdentityService identityService) : IRequestHandler<GetBasketQuery, ServiceResult<BasketDto>>
   
    {
        public async Task<ServiceResult<BasketDto>> Handle(GetBasketQuery request,
            CancellationToken cancellationToken)
        {
            var userId = identityService.GetUserId;
            var cacheKey = string.Format(ConstBasket.BasketCacheKey, userId);

            var basketAsString = await distributedCache.GetStringAsync(cacheKey, token: cancellationToken);

            if (string.IsNullOrEmpty(basketAsString))
            {
                return ServiceResult<BasketDto>.Error("Basket not found", HttpStatusCode.NotFound);
            }

            var basket = JsonSerializer.Deserialize<BasketDto>(basketAsString)!;

            var basketDto = basket.Adapt<BasketDto>();


            return ServiceResult<BasketDto>.SuccessAsOk(basketDto);
        }
    }
}
