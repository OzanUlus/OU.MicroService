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
    public class GetBasketQueryHandler(BasketService basketService) : IRequestHandler<GetBasketQuery, ServiceResult<BasketDto>>
   
    {
        public async Task<ServiceResult<BasketDto>> Handle(GetBasketQuery request,
            CancellationToken cancellationToken)
        {
        

            var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);

            if (string.IsNullOrEmpty(basketAsJson))
            {
                return ServiceResult<BasketDto>.Error("Basket not found", HttpStatusCode.NotFound);
            }

            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson)!;

            var basketDto = basket.Adapt<BasketDto>();


            return ServiceResult<BasketDto>.SuccessAsOk(basketDto);
        }
    }
}
