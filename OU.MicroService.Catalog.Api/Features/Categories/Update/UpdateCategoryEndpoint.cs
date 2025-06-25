using MediatR;
using OU.Microservice.Shared.Extensions;
using OU.Microservice.Shared.Filters;

namespace OU.MicroService.Catalog.Api.Features.Categories.Update
{
    public static class UpdateCategoryEndpoint
    {
        public static RouteGroupBuilder UpdateCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {

            group.MapPut("/{id:guid}", async (UpdateCategoryCommand comand, IMediator mediator, Guid id) => (await mediator.Send(comand)).ToGenericResult()).WithName("UpdateCategory").MapToApiVersion(1, 0).AddEndpointFilter<ValidationFilter<UpdateCategoryCommand>>();

            return group;

        }
    }
}
