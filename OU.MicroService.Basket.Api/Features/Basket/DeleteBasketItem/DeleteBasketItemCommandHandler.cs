using MediatR;
using OU.Microservice.Shared;
using OU.Microservice.Shared.Services;
using System.Net;
using System.Text.Json;

namespace OU.MicroService.Basket.Api.Features.Basket.DeleteBasketItem
{
    public class DeleteBasketItemCommandHandler(BasketService basketService) : IRequestHandler<DeleteBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            

            var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);

            if(string.IsNullOrEmpty(basketAsJson))
            {
                return ServiceResult.Error("Basket not found", System.Net.HttpStatusCode.NotFound);
            }

            var currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson);

            var basketItemToDelete = currentBasket!.BasketItems.FirstOrDefault(x => x.Id == request.Id);

            if (basketItemToDelete is null)
            {
                return ServiceResult.Error("Basket item not found", HttpStatusCode.NotFound);
            }

            currentBasket.BasketItems.Remove(basketItemToDelete);

            basketAsJson = JsonSerializer.Serialize(currentBasket);

            await basketService.CreateBasketCacheAsync(currentBasket,cancellationToken);


            return ServiceResult.SuccessAsNoContent();
        }
    }
}
