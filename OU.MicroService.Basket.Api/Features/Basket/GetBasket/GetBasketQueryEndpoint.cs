using MediatR;
using OU.Microservice.Shared.Extensions;

namespace OU.MicroService.Basket.Api.Features.Basket.GetBasket
{
    public static class GetBasketQueryEndpoint
    {
        public static RouteGroupBuilder GetBasketGroupItemEndpoint(this RouteGroupBuilder group)
        {

            group.MapGet("/user", async (IMediator mediator) => (await mediator.Send(new GetBasketQuery())).ToGenericResult())
                .WithName("GetBasket")
                  .MapToApiVersion(1, 0);

            return group;
        }
    }
}
