﻿using Asp.Versioning.Builder;
using MediatR;
using OU.Microservice.Shared.Extensions;
using OU.Microservice.Shared.Filters;

namespace OU.MicroService.Catalog.Api.Features.Categories.Create
{
    public static class CreateCategoryEndpoint
    {
        public static RouteGroupBuilder CreateCategoryGroupItemEndpoint(this RouteGroupBuilder group) 
        {

            group.MapPost("/", async (CreateCategoryCommand comand, IMediator mediator) => (await mediator.Send(comand)).ToGenericResult())
                .WithName("CreateCategory")
                  .MapToApiVersion(1, 0)
                .AddEndpointFilter<ValidationFilter<CreateCategoryCommand>>();

            return group;

        }
    }
}
