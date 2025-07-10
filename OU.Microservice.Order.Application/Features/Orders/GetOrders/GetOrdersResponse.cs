using OU.Microservice.Order.Application.Features.Orders.Create;

namespace OU.Microservice.Order.Application.Features.Orders.GetOrders
{
    public record GetOrdersResponse(DateTime Created, decimal TotalPrice, List<OrderItemDto> Items);
}
