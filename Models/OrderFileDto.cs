using ShopTI.Entities;

namespace ShopTI.Models
{
    public class OrderFileDto
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string PaymentMethod { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
