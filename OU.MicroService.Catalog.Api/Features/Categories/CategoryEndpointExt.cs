using OU.Microservice.Shared.Filters;
using OU.MicroService.Catalog.Api.Features.Categories.Create;

namespace OU.MicroService.Catalog.Api.Features.Categories
{
    public static class CategoryEndpointExt
    {
        public static void AddCategoryGroupEndpointExt(this WebApplication app) 
        {

            app.MapGroup("api/categories").CreateCategoryGroupItemEndpoint();


        }
    }
}
