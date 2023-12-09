using CarMarketApi.Dtos.PersonalInformationDtos;

namespace CarMarketApi.Dtos.SellerDtos
{
    public class SellerPutDto : PersonalInformationOnlyDto
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public int? Age { get; set; }

        public List<int>? BuyerIds { get; set; }

        public List<int>? ItemIds { get; set; }
    }
}
