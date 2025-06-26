namespace OU.MicroService.Basket.Api.Dtos
{
    public record BasketDto(Guid UserId, List<BasketItemDto> BasketItems)
    {
    }
}
