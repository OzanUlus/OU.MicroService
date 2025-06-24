using MediatR;
using OU.Microservice.Shared;
using OU.MicroService.Catalog.Api.Features.Categories.GetById;
using OU.MicroService.Catalog.Api.Repositories;

namespace OU.MicroService.Catalog.Api.Features.Categories.Delete
{
    public record DeleteCategoryCommand(Guid Id) : IRequestByServiceResult;

    public class DeleteategoryCommandHandler(AppDbContext context) : IRequestHandler<DeleteCategoryCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await context.Categories.FindAsync(request.Id);
            if (category == null) return ServiceResult.Error("Category not found.", $"The Category id: '{request.Id}' was not found", System.Net.HttpStatusCode.NotFound);

            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return ServiceResult.SuccessAsNoContent();
        }
    }
    public static class DeleteCategoryEndpoint
    {
        public static RouteGroupBuilder DeleteCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {

            group.MapDelete("/{id:guid}", async (IMediator mediator, Guid id) => (await mediator.Send(new DeleteCategoryCommand(id))));

            return group;

        }
    }
}
