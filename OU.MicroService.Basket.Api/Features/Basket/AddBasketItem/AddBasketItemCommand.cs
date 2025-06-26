using OU.Microservice.Shared;

namespace OU.MicroService.Basket.Api.Features.Basket.AddBasketItem
{
    public record AddBasketItemCommand(Guid CourseId, string CourseName, decimal CoursePrice, string? ImageUrl) : IRequestByServiceResult;
   
}
