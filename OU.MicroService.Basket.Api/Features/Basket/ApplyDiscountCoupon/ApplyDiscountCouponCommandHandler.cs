using MediatR;
using OU.Microservice.Shared;
using OU.MicroService.Basket.Api.Dtos;
using System.Net;
using System.Text.Json;

namespace OU.MicroService.Basket.Api.Features.Basket.ApplyDiscountCoupon
{
    public class ApplyDiscountCouponCommandHandler(
        BasketService basketService)
        : IRequestHandler<ApplyDiscountCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(ApplyDiscountCouponCommand request, CancellationToken cancellationToken)
        {
            var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);


            if (string.IsNullOrEmpty(basketAsJson))
            {
                return ServiceResult<BasketDto>.Error("Basket not found", HttpStatusCode.NotFound);
            }

            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson)!;

            if (!basket.BasketItems.Any())
            {
                return ServiceResult<BasketDto>.Error("Basket item not found", HttpStatusCode.NotFound);
            }

            basket.ApplyNewDiscount(request.Coupon, request.DiscountRate);

            basketAsJson = JsonSerializer.Serialize(basket);

            await basketService.CreateBasketCacheAsync(basket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
