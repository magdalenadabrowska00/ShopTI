using ShopTI.Entities;
using ShopTI.Models;

namespace ShopTI.IServices
{
    public interface IOrderService
    {
        List<Order> GetOrders();
        OrderDto CreateOrder(OrderDto request);
    }
}
