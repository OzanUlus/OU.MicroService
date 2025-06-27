using MediatR;
using Microsoft.AspNetCore.Mvc;
using OU.Microservice.Shared.Extensions;
using OU.Microservice.Shared.Filters;

namespace OU.MicroService.Discount.Api.Features.Discounts.CreateDiscount
{
    public static class CreateDiscountEndpoint
    {
        public static RouteGroupBuilder CreateDiscountGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/",
                    async (CreateDiscountCommand command, IMediator mediator) =>
                        (await mediator.Send(command)).ToGenericResultToResult())
                .WithName("CreateDiscount")
                .MapToApiVersion(1, 0)
                .Produces<Guid>(StatusCodes.Status201Created)
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
                .AddEndpointFilter<ValidationFilter<CreateDiscountCommandValidator>>();

            return group;
        }
    }
}
