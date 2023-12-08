using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarMarketApi.Entities
{
    [Table("Users", Schema = "market")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(20)]
        public string Surname { get; set; } 

        public virtual ICollection<Car> Cars { get; set; }
        public virtual PrivateInformation PrivateInformation { get; set; }
        public virtual ICollection<SellersUsersJoin> SellersUsers { get; set; }
    }
}
