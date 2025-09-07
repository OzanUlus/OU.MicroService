using MassTransit;
using OU.Microservice.Bus.Events;
using OU.MicroService.Basket.Api.Features.Basket;

namespace OU.MicroService.Basket.Api.Consumers
{
    public class OrderCreatedEventConsumer(IServiceProvider serviceProvider) : IConsumer<OrderCreatedEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            using var scope = serviceProvider.CreateScope();
            var basketService = scope.ServiceProvider.GetRequiredService<BasketService>();
            await basketService.DeleteBasket(context.Message.Userıd);
        }
    }
}
