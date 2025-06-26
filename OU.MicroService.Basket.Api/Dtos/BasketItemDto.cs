namespace OU.MicroService.Basket.Api.Dtos
{
    public record BasketItemDto(Guid Id, string CourseName, string ImageUrl, decimal Price, decimal? PriceByApplyDiscountRate);
    
    
}
