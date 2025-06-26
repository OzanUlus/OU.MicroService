using OU.Microservice.Shared;
using OU.MicroService.Basket.Api.Dtos;

namespace OU.MicroService.Basket.Api.Features.Basket.GetBasket
{
    public record GetBasketQuery : IRequestByServiceResult<BasketDto>;
}
