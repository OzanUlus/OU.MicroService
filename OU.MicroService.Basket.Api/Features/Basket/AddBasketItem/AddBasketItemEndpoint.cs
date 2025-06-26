using MediatR;
using OU.Microservice.Shared.Extensions;
using OU.Microservice.Shared.Filters;


namespace OU.MicroService.Basket.Api.Features.Basket.AddBasketItem
{
    public static class AddBasketItemEndpoint
    {
        public static RouteGroupBuilder AddBasketItemGroupItemEndpoint(this RouteGroupBuilder group)
        {

            group.MapPost("/item", async (AddBasketItemCommand comand, IMediator mediator) => (await mediator.Send(comand)).ToGenericResultToResult())
                .WithName("AddBasketItem")
                  .MapToApiVersion(1, 0)
                .AddEndpointFilter<ValidationFilter<AddBasketItemCommandValidator>>();

            return group;

        }
    }
}
