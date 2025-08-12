using Asp.Versioning.Builder;
using OU.Microservice.Order.Api.Endpoints.Orders;

namespace OU.MicroService.Catalog.Api.Features.Courses
{
    public static class OrderEndpointExt
    {
        public static void AddOrderGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/orders").WithTags("Orders")
                .WithApiVersionSet(apiVersionSet)
                .CreateOrderGroupItemEndpoint()
                .GetOrdersGroupItemEndpoint().RequireAuthorization();
        }
    }
}
