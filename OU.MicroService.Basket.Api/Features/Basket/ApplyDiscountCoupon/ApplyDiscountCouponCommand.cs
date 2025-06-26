using OU.Microservice.Shared;

namespace OU.MicroService.Basket.Api.Features.Basket.ApplyDiscountCoupon
{
    public record ApplyDiscountCouponCommand(string Coupon, float DiscountRate) : IRequestByServiceResult;
}
