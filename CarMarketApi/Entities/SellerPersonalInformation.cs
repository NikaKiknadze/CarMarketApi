using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarMarketApi.Entities
{
    [Table("SellersPersonalInformations", Schema = "market")]
    public class SellerPersonalInformation
    {
        [Key]
        public int Id { get; set; }
        [Phone]
        public int PhoneNumber { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        [ForeignKey("SellerId")]
        public int? SellerId { get; set; }
        public virtual Seller? Seller { get; set; }
    }
}
