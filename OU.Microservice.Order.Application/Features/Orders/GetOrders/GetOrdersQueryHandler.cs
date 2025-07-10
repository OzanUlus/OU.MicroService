using Mapster;
using MapsterMapper;
using MediatR;
using OU.Microservice.Order.Application.Contracts.Repositories;
using OU.Microservice.Order.Application.Features.Orders.Create;
using OU.Microservice.Shared;
using OU.Microservice.Shared.Services;

namespace OU.Microservice.Order.Application.Features.Orders.GetOrders
{
    public class GetOrdersQueryHandler(IIdentityService identityService, IOrderRepository orderRepository) : IRequestHandler<GetOrdersQuery, ServiceResult<List<GetOrdersResponse>>>
    {
        public async Task<ServiceResult<List<GetOrdersResponse>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await orderRepository.GetOrderByBuyerId(identityService.GetUserId);


            var response = orders
    .Select(o => new GetOrdersResponse(
        o.Created,
        o.TotalPrice,
        o.OrderItems.Adapt<List<OrderItemDto>>()
    ))
    .ToList();


            return ServiceResult<List<GetOrdersResponse>>.SuccessAsOk(response);
        }
    }
}
