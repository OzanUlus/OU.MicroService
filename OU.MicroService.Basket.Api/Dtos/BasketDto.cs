using System.Text.Json.Serialization;

namespace OU.MicroService.Basket.Api.Dtos
{
    public record BasketDto
    {
      
        public List<BasketItemDto> BasketItems { get; set; } = new();
        public float? DiscountRate { get; set; }
        public string? Coupon { get; set; }


        public decimal TotalPrice => BasketItems.Sum(x => x.Price);

        [JsonIgnore]
        public bool IsApplyDiscount => DiscountRate is > 0 && !string.IsNullOrEmpty(Coupon);
        public decimal? TotalPriceWithAppliedDiscount => !IsApplyDiscount ? null : BasketItems.Sum(x => x.PriceByApplyDiscountRate);

        public BasketDto(List<BasketItemDto> basketItems) 
        {
           
            BasketItems = basketItems;
        }

        public BasketDto()
        {
            
        }


    }
}
