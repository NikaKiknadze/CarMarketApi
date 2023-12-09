using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarMarketApi.Entities
{
    [Table("BuyersPersonalInformations", Schema = "market")]
    public class BuyerPersonalInformation
    {
        [Key]
        public int Id { get; set; }
        [Phone]
        public int PhoneNumber { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        [ForeignKey("UserId")]
        public virtual Buyer? Buyer { get; set; }

    }
}
