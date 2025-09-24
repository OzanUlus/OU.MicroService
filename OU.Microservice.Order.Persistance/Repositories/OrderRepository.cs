using Microsoft.EntityFrameworkCore;
using OU.Microservice.Order.Application.Contracts.Repositories;
using OU.Microservice.Order.Domain.Entities;

namespace OU.Microservice.Order.Persistance.Repositories
{
    public class OrderRepository(AppDbContext context) : GenericRepository<Guid, Domain.Entities.Order>(context), IOrderRepository
    {
        public Task<List<Domain.Entities.Order>> GetOrderByBuyerId(Guid buyerId)
        {
            return context.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == buyerId).OrderByDescending(x => x.Created).ToListAsync();
        }

        public async Task SetStatus(string orderCode, Guid paymentId, OrderStatus status)
        {
            var order = await context.Orders.FirstAsync(x => x.Code == orderCode);
           
            order.Status = status;
            order.PaymentId = paymentId;
            context.Orders.Update(order);
           

        }
    }
}
