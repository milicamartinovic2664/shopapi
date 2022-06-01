using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApi.Models
{
    public class JewleryItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string? Name { get; set; }

        public string? Type { get; set; }

        public decimal Price { get; set; }

        [ForeignKey("Manufacturer")]
        public long? ManufacturerId { get; set; }
        
        public Manufacturer? Manufacturer { get; set; }
    }
}
