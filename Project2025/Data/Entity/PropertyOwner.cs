using System.ComponentModel.DataAnnotations;

namespace Project2025.Data.Entity
{
    public class PropertyOwner
    {
        [Key]
        public Guid ownerId { get; set; }
        public string ownerName { get; set; }
        public string email { get; set; }
        public int phone { get; set; }
        public List<Chalet> Chalets { get; set; } = [];
    }
}