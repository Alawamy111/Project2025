using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project2025.Data.Entity
{
    public class Chalet
    {
        [Key]
        public Guid ChaletId { get; set; }

        public double price { get; set; }
        public double area { get; set; }
        public string location { get; set; }

        public Guid OwnerId { get; set; } // ✅ المفتاح الأجنبي الجديد

        [ForeignKey("OwnerId")]
        public PropertyOwner? PropertyOwner { get; set; }
    }
}
