using Asp.Versioning.Builder;
using OU.MicroService.Catalog.Api.Features.Categories.Create;
using OU.MicroService.Catalog.Api.Features.Categories.Delete;
using OU.MicroService.Catalog.Api.Features.Categories.GetAll;
using OU.MicroService.Catalog.Api.Features.Categories.GetById;
using OU.MicroService.Catalog.Api.Features.Categories.Update;

namespace OU.MicroService.Catalog.Api.Features.Categories
{
    public static class CategoryEndpointExt
    {
        public static void AddCategoryGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet) 
        {

            app.MapGroup("api/v{version:apiVersion}/categories").WithTags("Categories")
                .WithApiVersionSet(apiVersionSet)
                .CreateCategoryGroupItemEndpoint()
                .GetAllCategoryGroupItemEndpoint()
                .GetByIdCategoryGroupItemEndpoint()
                .UpdateCategoryGroupItemEndpoint()
                .DeleteCategoryGroupItemEndpoint();


        }
    }
}
