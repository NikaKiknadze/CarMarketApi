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

        public virtual PrivateInformation PrivateInformation { get; set; } 
        public virtual ICollection<SellersUsersJoin> SellersUsers {  get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }
}
