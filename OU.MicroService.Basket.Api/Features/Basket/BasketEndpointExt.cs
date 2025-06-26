using Asp.Versioning.Builder;
using OU.MicroService.Basket.Api.Features.Basket.AddBasketItem;
using OU.MicroService.Basket.Api.Features.Basket.ApplyDiscountCoupon;
using OU.MicroService.Basket.Api.Features.Basket.DeleteBasketItem;
using OU.MicroService.Basket.Api.Features.Basket.GetBasket;
using OU.MicroService.Basket.Api.Features.Basket.RemoveDiscountCoupon;

namespace OU.MicroService.Basket.Api.Features.Basket
{
    public static class BasketEndpointExt
    {
        public static void AddBasketGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {

            app.MapGroup("api/v{version:apiVersion}/baskets").WithTags("Baskets")
                .WithApiVersionSet(apiVersionSet)
                .AddBasketItemGroupItemEndpoint()
                .DeleteBasketItemGroupItemEndpoint()
                .GetBasketGroupItemEndpoint()
                .ApplyDiscountCouponGroupItemEndpoint()
                .RemoveDiscountCouponGroupItemEndpoint();




        }
    }
}
