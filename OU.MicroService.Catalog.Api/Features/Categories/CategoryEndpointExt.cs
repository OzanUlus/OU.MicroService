using OU.MicroService.Catalog.Api.Features.Categories.Create;
using OU.MicroService.Catalog.Api.Features.Categories.GetAll;
using OU.MicroService.Catalog.Api.Features.Categories.GetById;

namespace OU.MicroService.Catalog.Api.Features.Categories
{
    public static class CategoryEndpointExt
    {
        public static void AddCategoryGroupEndpointExt(this WebApplication app) 
        {

            app.MapGroup("api/categories")
                .CreateCategoryGroupItemEndpoint()
                .GetAllCategoryGroupItemEndpoint()
                .GetByIdCategoryGroupItemEndpoint();


        }
    }
}
