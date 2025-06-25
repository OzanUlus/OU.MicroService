using OU.MicroService.Catalog.Api.Features.Categories.Dtos;

namespace OU.MicroService.Catalog.Api.Features.Courses.Dtos
{
    public record CourseDto(
       Guid Id,
       string Name,
       string Description,
       decimal Price,
       string ImageUrl,
       CategoryDto Category,
       FeatureDto Feature);
}
