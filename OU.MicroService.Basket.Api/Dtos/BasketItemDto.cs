namespace OU.MicroService.Basket.Api.Dtos
{
    public record BasketItemDto(Guid CourseId, string CourseName, string ImageUrl, decimal Price, decimal? PriceByApplyDiscountRate);
    
    
}
