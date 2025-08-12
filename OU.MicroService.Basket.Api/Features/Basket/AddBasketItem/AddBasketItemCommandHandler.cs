using MediatR;
using OU.Microservice.Shared;
using OU.Microservice.Shared.Services;
using OU.MicroService.Basket.Api.Data;
using System.Text.Json;

namespace OU.MicroService.Basket.Api.Features.Basket.AddBasketItem
{
    public class AddBasketItemCommandHandler(IIdentityService identityService, BasketService basketService) : IRequestHandler<AddBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
        

            var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);


            Data.Basket? currentBasket;

            var newBasketItem = new BasketItem(request.CourseId, request.CourseName, request.ImageUrl, request.CoursePrice, null);

            if (string.IsNullOrEmpty(basketAsJson))
            {

                currentBasket = new Data.Basket(identityService.UserId, [newBasketItem]);

                await basketService.CreateBasketCacheAsync(currentBasket,cancellationToken);

                return ServiceResult.SuccessAsNoContent();

            }
            
                currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson);

                var existingBasketItem = currentBasket.BasketItems.FirstOrDefault(x => x.Id == request.CourseId);

                if (existingBasketItem is not null)
                {
                    currentBasket.BasketItems.Remove(existingBasketItem);
                }
             
                    currentBasket.BasketItems.Add(newBasketItem);
                    currentBasket.ApplyAvaliableDiscount();


            await basketService.CreateBasketCacheAsync(currentBasket ,cancellationToken);

            return ServiceResult.SuccessAsNoContent();




        }
       
    }
}
