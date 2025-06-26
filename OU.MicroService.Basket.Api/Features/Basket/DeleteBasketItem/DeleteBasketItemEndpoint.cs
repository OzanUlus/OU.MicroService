using MediatR;
using Microsoft.AspNetCore.Mvc;
using OU.Microservice.Shared.Extensions;

namespace OU.MicroService.Basket.Api.Features.Basket.DeleteBasketItem
{
    public static class DeleteBasketItemEndpoint
    {
        public static RouteGroupBuilder DeleteBasketItemGroupItemEndpoint(this RouteGroupBuilder group)
        {

            group.MapDelete("/item/{id:guid}", async (Guid id, [FromServices] IMediator mediator) => (await mediator.Send(new DeleteBasketItemCommand(id))).ToGenericResultToResult())
                .WithName("DeleteBasketItem")
                  .MapToApiVersion(1, 0);

            return group;
        }
    }
}
