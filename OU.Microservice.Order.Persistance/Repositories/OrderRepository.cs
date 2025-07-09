using Microsoft.EntityFrameworkCore;
using OU.Microservice.Order.Application.Contracts.Repositories;

namespace OU.Microservice.Order.Persistance.Repositories
{
    public class OrderRepository(AppDbContext context) : GenericRepository<Guid, Domain.Entities.Order>(context), IOrderRepository
    {
        public Task<List<Domain.Entities.Order>> GetOrderByBuyerId(Guid buyerId)
        {
            return context.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == buyerId).OrderByDescending(x => x.Created).ToListAsync();
        }
    }
}
