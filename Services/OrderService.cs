using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ShopTI.Entities;
using ShopTI.IServices;
using ShopTI.Models;

namespace ShopTI.Services
{
    public class OrderService : IOrderService
    {
        private readonly ShopDbContext _dbcontext;
        public OrderService(
            ShopDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public List<Order> GetOrders()
        {
            return _dbcontext.Orders.Include(x => x.User).ToList(); //możliwe że bez include'a
        }

        public OrderDto CreateOrder(OrderDto request)
        {
            var user = _dbcontext.Users.FirstOrDefault(x => x.Email == request.UserEmail);

            if (user == null)
            {
                Log.Error("Użytkownik o podanym adresie email nie istnieje;{0}", request.UserEmail);
                throw new Exception($"Nie istnieje użytkownik o adresie email: {request.UserEmail}");
            }
                
            var order = new Order
            {
                OrderId = request.OrderId,
                PaymentMethod = request.PaymentMethod,
                User = user,
                UserId = user.UserId
            };

            order = (_dbcontext.Orders.Add(order)).Entity;
            _dbcontext.SaveChanges();

            var orderDetails = request.OrderDetails.Select(y => new OrderDetail
            {
                OrderId = order.OrderId,
                Product = _dbcontext.Products.FirstOrDefault(x => x.ProductId == y.ProductId),
                ProductId = y.ProductId,
                ProductPrice = y.ProductPrice,
                Quantity = y.Quantity
            });
            
            _dbcontext.OrderDetails.AddRange(orderDetails);
            order.TotalPrice = orderDetails.Sum(x => x.Quantity * x.ProductPrice);

            _dbcontext.SaveChanges();

            Log.Information("Użytkownik o podanym emailu złożył zamówienie o podanym numerze;{0};{1}", request.UserEmail, order.OrderId);
            return request;
        }
    }
}
