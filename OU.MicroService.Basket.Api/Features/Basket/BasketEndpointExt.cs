using Asp.Versioning.Builder;
using OU.MicroService.Basket.Api.Features.Basket.AddBasketItem;

namespace OU.MicroService.Basket.Api.Features.Basket
{
    public static class BasketEndpointExt
    {
        public static void AddBasketGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {

            app.MapGroup("api/v{version:apiVersion}/baskets").WithTags("Baskets")
                .WithApiVersionSet(apiVersionSet)
                .AddBasketItemGroupItemEndpoint();




        }
    }
}
