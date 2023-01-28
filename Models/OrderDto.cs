using ShopTI.Entities;

namespace ShopTI.Models
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        //public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string PaymentMethod { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderDetailDto> OrderDetails { get; set; }
    }
}
