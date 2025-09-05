using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OU.Microservice.Shared;
using OU.MicroService.Catalog.Api.Repositories;

namespace OU.MicroService.Catalog.Api.Features.Categories.Create
{
    public class CreateCategoryCommandHandler(AppDbContext context) : IRequestHandler<CreateCategoryCommand, ServiceResult<CreateCategoryResponse>>
    {
        public async Task<ServiceResult<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var existCategory = await context.Categories.AnyAsync(ca => ca.Name == request.Name, cancellationToken: cancellationToken);

            if (existCategory) 
            {
                ServiceResult<CreateCategoryResponse>.Error("Category Name already exists", $"The category name '{request.Name}' already exists", System.Net.HttpStatusCode.BadRequest);
            }

            Category category = new Category { Name = request.Name, Id = Guid.CreateVersion7() };

            await context.Categories.AddAsync(category, cancellationToken);   
            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(new CreateCategoryResponse(category.Id), "<empty>");
        }
    }
}
