using CarMarketApi.Dtos.PersonalInformationDtos;
using CarMarketApi.Entities;

namespace CarMarketApi.Dtos.BuyerDtos
{
    public class BuyerGetDto
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public int? Age { get; set; }

        public PersonalInformationDtos.PersonalInformationOnlyDto? PersonalInformation { get; set; }

        public List<SellerDtos.SellerOnlyDto>? Sellers { get; set; }

        public List<ItemDtos.ItemOnlyDto>? Items { get; set; }

    }
}
