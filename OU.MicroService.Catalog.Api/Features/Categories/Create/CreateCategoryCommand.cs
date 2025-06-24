using OU.Microservice.Shared;

namespace OU.MicroService.Catalog.Api.Features.Categories.Create
{
    public record CreateCategoryCommand(string Name) : IRequestByServiceResult<CreateCategoryResponse>;
    
   
}
