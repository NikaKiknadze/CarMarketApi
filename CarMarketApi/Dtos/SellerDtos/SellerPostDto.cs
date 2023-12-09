using CarMarketApi.Dtos.PersonalInformationDtos;

namespace CarMarketApi.Dtos.SellerDtos
{
    public class SellerPostDto : PersonalInformationWithoutId
    {
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public int? Age { get; set; }

        public List<int>? BuyerIds { get; set; }

        public List<int>? ItemIds { get; set; }
    }
}
