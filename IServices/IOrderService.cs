using ShopTI.Entities;
using ShopTI.Models;

namespace ShopTI.IServices
{
    public interface IOrderService
    {
        int CreateNewOrder(NewOrderModel newOrder);
        List<Order> GetOrders();
        Order CreateOrder(Order order);
        void PutOrder(int id, Order order);
    }
}
