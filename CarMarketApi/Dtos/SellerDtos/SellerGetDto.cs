namespace CarMarketApi.Dtos.SellerDtos
{
    public class SellerGetDto
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public int? Age { get; set; }

        public List<BuyerDtos.BuyerOnlyDto>? Buyers { get; set; }

        public List<ItemDtos.ItemOnlyDto>? Items { get; set; }

        public PersonalInformationDtos.PersonalInformationOnlyDto? PersonalInformation { get; set; }
    }
}
