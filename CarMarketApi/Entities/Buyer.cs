using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarMarketApi.Entities
{
    [Table("Buyers", Schema = "market")]
    public class Buyer
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(20)]
        public string Surname { get; set; }
        public int Age { get; set; }
        [ForeignKey("PersonalInformationId")]
        public int? PersonalIformationId { get; set; }

        public virtual ICollection<Item> Items { get; set; }
        public virtual BuyerPersonalInformation? PersonalInformation { get; set; }
        public virtual ICollection<SellersBuyersJoin>? SellersBuyers { get; set; }
    }
}
