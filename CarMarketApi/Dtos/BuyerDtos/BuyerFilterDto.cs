namespace CarMarketApi.Dtos.BuyerDtos
{
    public class BuyerFilterDto : Pageing
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public int? Age { get; set; }

        public int? PersonalInformationId { get; set; }

        public List<int>? SellerIds { get; set; }

        public List<int>? ItemIds { get; set; }
    }
}
