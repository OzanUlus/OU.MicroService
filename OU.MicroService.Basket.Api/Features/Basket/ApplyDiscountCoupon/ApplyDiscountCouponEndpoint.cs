using MediatR;
using OU.Microservice.Shared.Extensions;
using OU.Microservice.Shared.Filters;

namespace OU.MicroService.Basket.Api.Features.Basket.ApplyDiscountCoupon
{
    public static class ApplyDiscountCouponEndpoint
    {
        public static RouteGroupBuilder ApplyDiscountCouponGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/apply-discount-coupon",
                    async (ApplyDiscountCouponCommand command, IMediator mediator) =>
                        (await mediator.Send(command)).ToGenericResultToResult())
                .WithName("ApplyDiscountCoupon")
                .MapToApiVersion(1, 0)
                .AddEndpointFilter<ValidationFilter<ApplyDiscountCouponCommand>>();
            return group;
        }
    }
}
