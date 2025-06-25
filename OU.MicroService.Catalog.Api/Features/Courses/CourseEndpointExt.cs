using OU.MicroService.Catalog.Api.Features.Courses.Create;
using OU.MicroService.Catalog.Api.Features.Courses.Delete;
using OU.MicroService.Catalog.Api.Features.Courses.GetAll;
using OU.MicroService.Catalog.Api.Features.Courses.GetAllByUserId.OU.MicroService.Catalog.Api.Features.Courses.getal;
using OU.MicroService.Catalog.Api.Features.Courses.GetById;
using OU.MicroService.Catalog.Api.Features.Courses.Update;

namespace OU.MicroService.Catalog.Api.Features.Courses
{
    public static class CourseEndpointExt
    {
        public static void AddCourseGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("api/courses").WithTags("Courses")
                .CreateCourseGroupItemEndpoint()
                .GetAllCourseGroupItemEndpoint()
                .GetByIdCourseGroupItemEndpoint()
                .UpdateCourseGroupItemEndpoint()
                .DeleteCourseGroupItemEndpoint()
                .GetByUserIdCourseGroupItemEndpoint();
        }
    }
}
