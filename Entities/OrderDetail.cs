namespace ShopTI.Entities
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int OrderId { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
    }
}
