using System.Text.Json.Serialization;

namespace OU.MicroService.Basket.Api.Dtos
{
    public record BasketDto
    {
      [JsonIgnore]  public Guid UserId { get; init; }
        public List<BasketItemDto> BasketItems { get; set; } = new();

        public BasketDto(Guid userId, List<BasketItemDto> basketItems) 
        {
            UserId = userId;
            BasketItems = basketItems;
        }

        public BasketDto()
        {
            
        }


    }
}
