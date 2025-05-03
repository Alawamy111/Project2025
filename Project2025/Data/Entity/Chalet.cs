namespace Project2025.Data.Entity
{
    public class Chalet
    {
        public Guid ChaletId { get; set; }

        public double price { get; set; }
        public double area { get; set; }
        public string location { get; set; }
        public Guid OwnerId { get; set; }
        public PropertyOwner PropertyOwner { get; set; } = new();
    }
}
