using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ShopTI.Entities;
using ShopTI.Enums;
using ShopTI.IServices;
using ShopTI.Models;

namespace ShopTI.Services
{
    public class OrderService : IOrderService
    {
        private readonly ShopDbContext _dbcontext;
        private readonly IUserContextService _userContextService;
        private readonly IAuthorizationService _authorizationService;
        public OrderService(
            ShopDbContext dbcontext,
            IUserContextService userContextService,
            IAuthorizationService authorizationService)
        {
            _dbcontext = dbcontext;
            _userContextService = userContextService;
            _authorizationService = authorizationService;
        }

        public List<Order> GetOrders()
        {
            return _dbcontext.Orders.Include(x => x.User).ToList(); //możliwe że bez include'a
        }
        public int CreateNewOrder(NewOrderModel newOrder)
        {
            //var user = _userContextService.User.Identity.Name;
            //var userEmail =_userContextService.User.Claims.FirstOrDefault(x => x.Type == "Email").ToString();
            //var userId = _dbcontext.Users.FirstOrDefault(x=>x.Email == userEmail).UserId;
            //wyliczyć total price z produktów
            //var products = _dbcontext.Products.ToList();
            //return 1;

            /*
            var authorizationResult = _authorizationService
            .AuthorizeAsync(_userContextService.User, entity, new ResourceOperationRequirement(ResourceOperation.Create))
            .Result;
           
            if (!authorizationResult.Succeeded)
            {
                Log.Error("Błąd przy autoryzacji użytkownika o id {0}.", _userContextService.GetUserId);
                throw new Exception("Błąd przy autoryzacji użytkownika.");
            }
             */


            
            //total price do wyliczenia z sumy price * quantity z każdego produktu z listy.
            //TotalPrice = newOrder.TotalPrice,

            return 1;
        }

        /*
        public ActionResult<Order> GetOrderMaster(int id)
        {
            var orderDetails = (from orderr in _dbcontext.Set<Order>()
                                     join detail in _dbcontext.Set<OrderDetail>()
                                     on orderr.OrderId equals detail.OrderId
                                     join product in _dbcontext.Set<Product>()
                                     on detail.ProductId equals product.ProductId
                                     where orderr.OrderId == id

                                     select new
                                     {
                                         orderr.OrderId,
                                         detail.OrderDetailId,
                                         detail.ProductId,
                                         detail.Quantity,
                                         detail.ProductPrice,
                                         product.ProductName
                                     }).ToList();

            var orderModel = (from a in _dbcontext.Set<Order>()
                                    where a.OrderId == id
                                    select new
                                    {
                                        a.OrderId,
                                        a.UserId,
                                        a.PaymentMethod,
                                        a.TotalPrice,
                                        deletedOrderItemIds = "",
                                        OrderDetails = orderDetails
                                    }).FirstOrDefault();

            var entity = _dbcontext.Orders.FirstOrDefault(x => x.OrderId == orderModel.OrderId);

            if (entity == null)
            {
                Log.Error("Nie istnieje zamówienie o numerze identyfikacyjnym: {0}.", id);
                throw new Exception("Nie ma takiego zamówienia.");
            }

            Log.Information("Pobrano zamówienie numer {0}.", id);
            return entity;
        }
        */

        public void PutOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                Log.Error("Nie istnieje zamówienie o numerze identyfikacyjnym: {0}.", id);
                throw new Exception("Nie ma takiego zamówienia.");
            }

            _dbcontext.Entry(order).State = EntityState.Modified;

            foreach (OrderDetail item in order.OrderDetails)
            {
                if (item.OrderDetailId == 0)
                {
                    _dbcontext.OrderDetails.Add(item);
                }
                else
                {
                    _dbcontext.Entry(item).State = EntityState.Modified;
                }
            }

            foreach (var i in order.DeletedOrderItemIds.Split(',').Where(x => x != ""))
            {
                OrderDetail y = _dbcontext.OrderDetails.Find(Convert.ToInt64(i));
                _dbcontext.OrderDetails.Remove(y);
            }

            try
            {
                _dbcontext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    Log.Error("Nie istnieje zamówienie o numerze identyfikacyjnym: {0}.", id);
                    throw new Exception("Błąd przy autoryzacji użytkownika.");
                }
                else
                {
                    Log.Information("Utworzono zamówienie o numerze identyfikacyjnym: {0}.", id);
                }
            }
        }

        public Order CreateOrder(Order order)
        {
            _dbcontext.Orders.Add(order);
            _dbcontext.SaveChanges();
            return order;
        }

        private bool OrderExists(int id)
        {
            return _dbcontext.Orders.Any(e => e.OrderId == id);
        }
    }
}
