using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarMarketApi.Entities
{
    [Table("Cars", Schema = "market")]
    public class Car
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        public string Manufacturer { get; set; }
        [MaxLength(20)]
        public string Model { get; set; }
        public int Year { get; set; }
        public int Value { get; set; }

        public virtual Seller Seller { get; set; }
        public virtual User User { get; set; }
    }
}
