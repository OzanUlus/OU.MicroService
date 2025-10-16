using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OU.Microservice.Shared;
using OU.Microservice.Shared.Extensions;
using OU.MicroService.Catalog.Api.Features.Categories.Dtos;
using OU.MicroService.Catalog.Api.Repositories;

namespace OU.MicroService.Catalog.Api.Features.Categories.GetAll
{
   
    public class GetAllCategoryQuery: IRequestByServiceResult<List<CategoryDto>>;


    public class GetAllCategoryQueryHandler(AppDbContext context) : IRequestHandler<GetAllCategoryQuery, ServiceResult<List<CategoryDto>>>
    {
        public async Task<ServiceResult<List<CategoryDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var categories = await context.Categories.ToListAsync();
            var categoryAsDto = categories.Adapt<List<CategoryDto>>();
            return ServiceResult<List<CategoryDto>>.SuccessAsOk(categoryAsDto);
        }
    }


    public static class GetAllCategoryEndpoint
    {
        public static RouteGroupBuilder GetAllCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {

            group.MapGet("/", async (IMediator mediator) => (await mediator.Send(new GetAllCategoryQuery())).ToGenericResult()).WithName("GetAllCategory")
                .MapToApiVersion(1, 0).RequireAuthorization(policyNames: "ClientCredential");

            return group;

        }
    }
}
