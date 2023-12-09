namespace CarMarketApi.Dtos.ItemDtos
{
    public class ItemGetDto
    {
        public int? Id { get; set; }

        public string? Type { get; set; }

        public int? Cost { get; set; }

        public SellerDtos.SellerOnlyDto? Seller {  get; set; } 

        public BuyerDtos.BuyerOnlyDto? Buyer { get; set; }
    }
}
