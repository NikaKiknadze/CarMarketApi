using CarMarketApi.Dtos.PersonalInformationDtos;

namespace CarMarketApi.Dtos.ItemDtos
{
    public class ItemPostDto
    {
        public string? Type { get; set; }

        public int? Cost { get; set; }

        public int? SellerId { get; set; }

        public int? BuyerId { get; set; }
    }
}
