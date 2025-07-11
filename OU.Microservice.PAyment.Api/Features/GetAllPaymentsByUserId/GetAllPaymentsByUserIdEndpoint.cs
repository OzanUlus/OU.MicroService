﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using OU.Microservice.Shared.Extensions;

namespace OU.Microservice.Payment.Api.Features.GetAllPaymentsByUserId
{
    public static class GetAllPaymentsByUserIdEndpoint
    {
        public static RouteGroupBuilder GetAllPaymentsByUserIdGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("",
                    async (IMediator mediator) =>
                        (await mediator.Send(new GetAllPaymentsByUserIdQuery())).ToGenericResult())
                .WithName("get-all-payments-by-userid")
                .MapToApiVersion(1, 0)
                .Produces(StatusCodes.Status200OK)
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);

            return group;
        }
    }
}
