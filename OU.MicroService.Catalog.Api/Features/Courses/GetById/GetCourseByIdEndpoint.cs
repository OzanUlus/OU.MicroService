using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OU.Microservice.Shared;
using OU.Microservice.Shared.Extensions;
using OU.MicroService.Catalog.Api.Features.Courses.Dtos;
using OU.MicroService.Catalog.Api.Repositories;
using System.Net;

namespace OU.MicroService.Catalog.Api.Features.Courses.GetById
{
    public record GetCourseByIdQuery(Guid Id) : IRequestByServiceResult<CourseDto>;


    public class GetCourseByIdQueryHandler(AppDbContext context)
        : IRequestHandler<GetCourseByIdQuery, ServiceResult<CourseDto>>
    {
        public async Task<ServiceResult<CourseDto>> Handle(GetCourseByIdQuery request,
            CancellationToken cancellationToken)
        {
            var hasCourse = await context.Courses.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);


            if (hasCourse is null)
            {
                return ServiceResult<CourseDto>.Error("Course not found",
                    $"The course with id({request.Id}) was not found", HttpStatusCode.NotFound);
            }

            var category = await context.Categories.FindAsync(hasCourse.CategoryId, cancellationToken);

            hasCourse.Category = category!;


            var courseAsDto = hasCourse.Adapt<CourseDto>();
            return ServiceResult<CourseDto>.SuccessAsOk(courseAsDto);
        }
    }

    public static class GetCourseByIdEndpoint
    {
        public static RouteGroupBuilder GetByIdCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}",
                    async (IMediator mediator, Guid id) =>
                        (await mediator.Send(new GetCourseByIdQuery(id))).ToGenericResult())
                .WithName("GetByIdCourses")
                /*.MapToApiVersion(1, 0)*/;

            return group;
        }
    }
}
