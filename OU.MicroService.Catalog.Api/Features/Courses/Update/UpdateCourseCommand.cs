using OU.Microservice.Shared;
using OU.MicroService.Catalog.Api.Features.Categories.Update;

namespace OU.MicroService.Catalog.Api.Features.Courses.Update
{
    public record UpdateCourseCommand(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        string? ImageUrl,
        Guid CategoryId) : IRequestByServiceResult<UpdateCategoryResponse>;
}
