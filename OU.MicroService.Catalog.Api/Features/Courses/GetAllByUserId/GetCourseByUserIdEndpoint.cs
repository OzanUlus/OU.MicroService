using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OU.Microservice.Shared;
using OU.Microservice.Shared.Extensions;
using OU.MicroService.Catalog.Api.Features.Courses.Dtos;
using OU.MicroService.Catalog.Api.Repositories;

namespace OU.MicroService.Catalog.Api.Features.Courses.GetAllByUserId
{
 

    namespace OU.MicroService.Catalog.Api.Features.Courses.getal
    {
        public record GetCourseByUserIdQuery(Guid Id) : IRequestByServiceResult<List<CourseDto>>;


        public class GetCourseByIdQueryHandler(AppDbContext context)
            : IRequestHandler<GetCourseByUserIdQuery, ServiceResult<List<CourseDto>>>
        {
            public async Task<ServiceResult<List<CourseDto>>> Handle(GetCourseByUserIdQuery request,
                CancellationToken cancellationToken)
            {
                var courses = await context.Courses.Where(x => x.UserId == request.Id)
                    .ToListAsync(cancellationToken: cancellationToken);

                var categories = await context.Categories.ToListAsync(cancellationToken: cancellationToken);


                foreach (var course in courses)
                {
                    course.Category = categories.First(x => x.Id == course.CategoryId);
                }

                var coursesAsDto = courses.Adapt<List<CourseDto>>();
                return ServiceResult<List<CourseDto>>.SuccessAsOk(coursesAsDto);
            }
        }

        public static class GetCourseByUserIdEndpoint
        {
            public static RouteGroupBuilder GetByUserIdCourseGroupItemEndpoint(this RouteGroupBuilder group)
            {
                group.MapGet("/user/{userId:guid}",
                        async (IMediator mediator, Guid userId) =>
                            (await mediator.Send(new GetCourseByUserIdQuery(userId))).ToGenericResult())
                    .WithName("GetByUserIdCourses")
                    /*.MapToApiVersion(1, 0)*/;

                return group;
            }
        }
    }

}
