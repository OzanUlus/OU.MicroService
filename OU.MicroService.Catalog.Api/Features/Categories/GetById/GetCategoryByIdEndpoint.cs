using Mapster;
using MediatR;
using OU.Microservice.Shared;
using OU.Microservice.Shared.Extensions;
using OU.MicroService.Catalog.Api.Features.Categories.Dtos;
using OU.MicroService.Catalog.Api.Repositories;

namespace OU.MicroService.Catalog.Api.Features.Categories.GetById
{
    public record GetCategoryByIdQuery(Guid Id) : IRequestByServiceResult<CategoryDto>;
    

    public class GetCategoryByIdQueryHandler(AppDbContext context) : IRequestHandler<GetCategoryByIdQuery, ServiceResult<CategoryDto>>
    {
        public async Task<ServiceResult<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await context.Categories.FindAsync(request.Id,cancellationToken);
            if (category == null) return ServiceResult<CategoryDto>.Error("Category not found.",$"The category with Id({request.Id}) was not found.",System.Net.HttpStatusCode.NotFound);
            var categoryAsDto = category.Adapt<CategoryDto>();
            return ServiceResult<CategoryDto>.SuccessAsOk(categoryAsDto);
        }
    }

    public static class GetCategoryByIdEndpoint
    {
        public static RouteGroupBuilder GetByIdCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {

            group.MapGet("/{id:guid}", async (IMediator mediator, Guid id) => (await mediator.Send(new GetCategoryByIdQuery(id))).ToGenericResult()).WithName("GetByIdCategory");

            return group;

        }
    }
}
