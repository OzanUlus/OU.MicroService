using Asp.Versioning.Builder;
using OU.MicroService.Discount.Api.Features.Discounts.CreateDiscount;
using OU.MicroService.Discount.Api.Features.Discounts.GetDiscountByCode;

namespace OU.MicroService.Discount.Api.Features.Courses
{
    public static class DiscountEndpointExt
    {
        public static void AddDiscountGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/discounts").WithTags("Discounts")
                .WithApiVersionSet(apiVersionSet)
                .CreateDiscountGroupItemEndpoint()
                .GetDiscountByCodeGroupItemEndpoint();
        }
    }
}
