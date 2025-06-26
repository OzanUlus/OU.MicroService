namespace OU.MicroService.Basket.Api.Data
{
    public class BasketItem
    {
        public BasketItem()
        {
            
        }
        public BasketItem(Guid ıd, string courseName, string? ımageUrl, decimal price, decimal? priceByApplyDiscountRate)
        {
            Id = ıd;
            CourseName = courseName;
            ImageUrl = ımageUrl;
            Price = price;
            PriceByApplyDiscountRate = priceByApplyDiscountRate;
        }

        public Guid Id { get; set; }
        public string CourseName { get; set; } = default!;
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal? PriceByApplyDiscountRate { get; set; }
    }
}
