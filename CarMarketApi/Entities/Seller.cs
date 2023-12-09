using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarMarketApi.Entities
{
    [Table("Sellers", Schema = "market")]
    public class Seller
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(20)]
        public string Surname { get; set; }
        public int Age { get; set; }
        [ForeignKey("PersonalInformationId")]
        public int? PersonalInformationId { get; set; }
        public virtual SellerPersonalInformation? SellerPersonalInformation { get; set; }
        public virtual ICollection<SellersBuyersJoin>? SellersBuyers {  get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
