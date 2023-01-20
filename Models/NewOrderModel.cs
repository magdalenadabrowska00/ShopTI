using ShopTI.Entities;

namespace ShopTI.Models
{
    public class NewOrderModel
    {
        public int UserId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal TotalPrice { get; set; }

        List<OrderDetail> OrderDetails { get; set; }
    }
}
