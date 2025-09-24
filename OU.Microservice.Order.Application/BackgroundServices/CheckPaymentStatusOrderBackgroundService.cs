using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OU.Microservice.Order.Application.Contracts.Refit.PaymentService;
using OU.Microservice.Order.Application.Contracts.Repositories;
using OU.Microservice.Order.Application.Contracts.UnitOfWork;

namespace OU.Microservice.Order.Application.BackgroundServices
{
    public class CheckPaymentStatusOrderBackgroundService(IServiceProvider serviceProvider) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = serviceProvider.CreateScope();

            var paymentservice = scope.ServiceProvider.GetRequiredService<IPaymentService>();
            var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            while (!stoppingToken.IsCancellationRequested)
            {
                var orders = orderRepository.Where(o => o.Status == Domain.Entities.OrderStatus.WaitingForPayment).ToList();

                foreach (var order in orders)
                {
                    var paymentStatusResponse = await paymentservice.GetPaymentStatus(order.Code);

                    if (paymentStatusResponse.isPaid)
                    {
                        await orderRepository.SetStatus(order.Code, paymentStatusResponse.PaymentId.Value, Domain.Entities.OrderStatus.Paid);
                        await unitOfWork.CommitAsync();
                    }
                }
                await Task.Delay(2000, stoppingToken);
            }

           

        }
    }
}
