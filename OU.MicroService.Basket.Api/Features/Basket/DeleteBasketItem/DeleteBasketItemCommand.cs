using OU.Microservice.Shared;

namespace OU.MicroService.Basket.Api.Features.Basket.DeleteBasketItem
{
    public record DeleteBasketItemCommand(Guid Id): IRequestByServiceResult;
    
    
}
