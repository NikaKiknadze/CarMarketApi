using System.ComponentModel.DataAnnotations.Schema;

namespace CarMarketApi.Entities
{
    [Table("SellersUsersJoin", Schema = "market")]
    public class SellersBuyersJoin
    {
        public int? BuyerId { get; set; }
        public int? SellerId { get; set; }
        public virtual Buyer? Buyer { get; set; }
        public virtual Seller? Seller { get; set; }
    }
}
