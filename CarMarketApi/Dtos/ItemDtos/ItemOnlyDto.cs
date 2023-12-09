using System.ComponentModel.DataAnnotations;

namespace CarMarketApi.Dtos.ItemDtos
{
    public class ItemOnlyDto
    {
        public int? Id { get; set; }
        
        public string? Type { get; set; }
       
        public int? Cost { get; set; }
    }
}
