using CarMarketApi.Dtos.PersonalInformationDtos;

namespace CarMarketApi.Dtos.BuyerDtos
{
    public class BuyerPutDto : PersonalInformationOnlyDto
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public int? Age { get; set; }

        public List<int>? SellerIds { get; set; }

        public List<int>? ItemIds { get; set; }
    }
}
