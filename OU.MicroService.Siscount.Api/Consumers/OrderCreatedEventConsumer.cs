using MassTransit;
using OU.Microservice.Bus.Events;
using OU.Microservice.Shared;
using OU.MicroService.Discount.Api.Features.Discounts;
using OU.MicroService.Discount.Api.Repositories;
using System.Threading;

namespace OU.MicroService.Discount.Api.Consumers
{
    public class OrderCreatedEventConsumer(IServiceProvider serviceProvider) : IConsumer<OrderCreatedEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            using var scope = serviceProvider.CreateScope();
            var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var discount = new Discount.Api.Features.Discounts.Discount()
            {
                Id = NewId.NextSequentialGuid(),
                Code = DiscountCodeGenerator.Generate(10),
                Rate = 0.1f,
                Created = DateTime.Now,
                Expired = DateTime.Now.AddMonths(1),
                UserId = context.Message.Userıd
            };

            await appDbContext.Discounts.AddAsync(discount);
            await appDbContext.SaveChangesAsync();

            
        }
    }
}
