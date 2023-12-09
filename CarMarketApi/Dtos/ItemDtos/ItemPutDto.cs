namespace CarMarketApi.Dtos.ItemDtos
{
    public class ItemPutDto
    {
        public int? Id { get; set; }

        public string? Type { get; set; }

        public int? Cost { get; set; }

        public int? SellerId { get; set; }

        public int? BuyerId { get; set; }
    }
}
