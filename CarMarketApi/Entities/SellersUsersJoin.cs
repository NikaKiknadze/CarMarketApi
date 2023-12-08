using System.ComponentModel.DataAnnotations.Schema;

namespace CarMarketApi.Entities
{
    [Table("SellersUsersJoin", Schema = "market")]
    public class SellersUsersJoin
    {
        public int UserId { get; set; }
        public int SellerId { get; set; }
        public virtual User User { get; set; }
        public virtual Seller Seller { get; set; }
    }
}
