using System.Text.Json.Serialization;

namespace OU.MicroService.Basket.Api.Data
{
    public class Basket
    {
        public Basket()
        {
            
        }
        public Basket(Guid userId, List<BasketItem> basketItems)
        {
            UserId = userId;
            BasketItems = basketItems;
        }

        public Guid UserId { get; set; }
        public List<BasketItem> BasketItems { get; set; } = new();
        public float? DiscountRate { get; set; }
        public string? Coupon { get; set; }


        [JsonIgnore]
        public bool IsApplyDiscount => DiscountRate is > 0 && !string.IsNullOrEmpty(Coupon);
        [JsonIgnore]
        public decimal TotalPrice => BasketItems.Sum(x => x.Price);


        [JsonIgnore]
        public decimal? TotalPriceWithAppliedDiscount => !IsApplyDiscount ? null : BasketItems.Sum(x => x.PriceByApplyDiscountRate);

     


        public void ApplyNewDiscount(string coupon, float disountRate) 
        {
        
            DiscountRate = disountRate;
            Coupon = coupon;

            foreach (var basket in BasketItems) 
            {

                basket.PriceByApplyDiscountRate = basket.Price * (decimal)(1 - disountRate);
            
            }
        
        }

        public void ApplyAvaliableDiscount()
        {
            if (!IsApplyDiscount) return;

            foreach (var basket in BasketItems)
            {

                basket.PriceByApplyDiscountRate = basket.Price * (decimal)(1 - DiscountRate!);

            }

        }

        public void ClearDiscount()
        {
            DiscountRate = null;
            Coupon = null;

            foreach (var basket in BasketItems)
            {

               basket.PriceByApplyDiscountRate = null;

            }

        }

    }
}
