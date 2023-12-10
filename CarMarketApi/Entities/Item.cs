using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarMarketApi.Entities
{
    [Table("Items", Schema = "market")]
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        public string Type { get; set; }
        [MaxLength(20)]
        public int Cost { get; set; }
        [ForeignKey("Seller")]
        public int? SellerId { get; set; }
        [ForeignKey("Buyer")]
        public int? BuyerId { get; set; }

        public virtual Seller? Seller { get; set; }
        public virtual Buyer? Buyer { get; set; }
    }
}
