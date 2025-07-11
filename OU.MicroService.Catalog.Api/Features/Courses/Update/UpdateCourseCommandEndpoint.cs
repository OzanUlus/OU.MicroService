﻿using MediatR;
using OU.Microservice.Shared.Filters;
using OU.Microservice.Shared.Extensions;

namespace OU.MicroService.Catalog.Api.Features.Courses.Update
{
    public static class UpdateCourseCommandEndpoint
    {
        public static RouteGroupBuilder UpdateCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/",
                    async (UpdateCourseCommand command, IMediator mediator) =>
                        (await mediator.Send(command)).ToGenericResult())
                .WithName("UpdateCourse")
                .MapToApiVersion(1, 0)
                .AddEndpointFilter<ValidationFilter<UpdateCourseCommand>>();

            return group;
        }
    }
}
