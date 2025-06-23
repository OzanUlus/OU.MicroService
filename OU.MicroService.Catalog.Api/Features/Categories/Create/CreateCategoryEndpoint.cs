using MediatR;
using OU.Microservice.Shared.Extensions;

namespace OU.MicroService.Catalog.Api.Features.Categories.Create
{
    public static class CreateCategoryEndpoint
    {
        public static RouteGroupBuilder CreateCategoryGroupItemEndpoint(this RouteGroupBuilder group) 
        {

            group.MapPost("/", async (CreateCategoryCommand comand, IMediator mediator) => (await mediator.Send(comand)).ToGenericResult());

            return group;

        }
    }
}
