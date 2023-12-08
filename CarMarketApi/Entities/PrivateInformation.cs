using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace CarMarketApi.Entities
{
    [Table("PrivateInformations", Schema = "market")]
    public class PrivateInformation
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        public int PersonalId { get; set; }
        public int MobilePhone { get; set; }
        public string Mail { get; set; }
        [MaxLength(20)]
        public string Surname { get; set; }

        public int? UserId { get; set; }
        public virtual User? User { get; set; }
        public int? SellerId { get; set; }
        public virtual Seller? Seller { get; set; }

    }
}
