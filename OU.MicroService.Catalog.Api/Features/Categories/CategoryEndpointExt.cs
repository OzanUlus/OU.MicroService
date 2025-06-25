using OU.MicroService.Catalog.Api.Features.Categories.Create;
using OU.MicroService.Catalog.Api.Features.Categories.Delete;
using OU.MicroService.Catalog.Api.Features.Categories.GetAll;
using OU.MicroService.Catalog.Api.Features.Categories.GetById;
using OU.MicroService.Catalog.Api.Features.Categories.Update;

namespace OU.MicroService.Catalog.Api.Features.Categories
{
    public static class CategoryEndpointExt
    {
        public static void AddCategoryGroupEndpointExt(this WebApplication app) 
        {

            app.MapGroup("api/categories").WithTags("Categories")
                .CreateCategoryGroupItemEndpoint()
                .GetAllCategoryGroupItemEndpoint()
                .GetByIdCategoryGroupItemEndpoint()
                .UpdateCategoryGroupItemEndpoint()
                .DeleteCategoryGroupItemEndpoint();


        }
    }
}
