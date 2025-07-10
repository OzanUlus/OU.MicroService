using OU.Microservice.Shared;

namespace OU.Microservice.Order.Application.Features.Orders.GetOrders
{
    public record GetOrdersQuery : IRequestByServiceResult<List<GetOrdersResponse>>;
}
