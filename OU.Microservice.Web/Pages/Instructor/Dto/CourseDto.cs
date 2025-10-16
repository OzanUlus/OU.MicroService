namespace OU.Microservice.Web.Pages.Instructor.Dto
{
    public record CourseDto(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string ImageUrl,
    DateTime Created,
    CategoryDto Category,
    FeatureDto Feature);
}
