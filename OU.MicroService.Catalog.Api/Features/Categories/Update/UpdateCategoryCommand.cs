using OU.Microservice.Shared;

namespace OU.MicroService.Catalog.Api.Features.Categories.Update
{
    public record UpdateCategoryCommand(Guid Id, string Name) : IRequestByServiceResult<UpdateCategoryResponse>
    {
    }
}
