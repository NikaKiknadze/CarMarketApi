using System.ComponentModel.DataAnnotations;

namespace CarMarketApi.Dtos.SellerDtos
{
    public class SellerOnlyDto
    {
        public int? Id { get; set; }
        
        public string? Name { get; set; }
        
        public string? Surname { get; set; }
        
        public int? Age { get; set; }
    }
}
