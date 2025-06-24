using Mapster;
using MediatR;
using OU.Microservice.Shared;
using OU.MicroService.Catalog.Api.Repositories;

namespace OU.MicroService.Catalog.Api.Features.Categories.Update
{
    public class UpdateCategoryCommandHandler(AppDbContext context) : IRequestHandler<UpdateCategoryCommand, ServiceResult<UpdateCategoryResponse>>
    {
        public async Task<ServiceResult<UpdateCategoryResponse>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await context.Categories.FindAsync(request.Id, cancellationToken);
            if (category == null) return ServiceResult<UpdateCategoryResponse>.Error("Category not found.",$"The Category Id: '{request.Id}' was not found.)", System.Net.HttpStatusCode.NotFound);

            category.Id = request.Id;
            category.Name = request.Name;

            context.Categories.Update(category);
            await context.SaveChangesAsync();

            var updateCategoryResponse = category.Adapt<UpdateCategoryResponse>();

            return ServiceResult<UpdateCategoryResponse>.SuccessAsOk(updateCategoryResponse);
        }
    }
}
