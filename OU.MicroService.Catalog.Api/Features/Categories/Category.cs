
using OU.MicroService.Catalog.Api.Features.Courses;
using OU.MicroService.Catalog.Api.Repositories;

namespace OU.MicroService.Catalog.Api.Features.Categories
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = default!;
        public List<Course>? Courses { get; set; }
    }
}
