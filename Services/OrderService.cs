﻿using Microsoft.AspNetCore.Authorization;
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

        public OrderDto CreateOrder(OrderDto request)
        {
            var user = _dbcontext.Users.FirstOrDefault(x => x.Email == request.UserEmail);

            if (user == null)
                throw new Exception("User not found");


            var order = new Order
            {
                OrderId = request.OrderId,
                PaymentMethod = request.PaymentMethod,
                //TotalPrice = request.TotalPrice,
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
                ProductPrice = _dbcontext.Products.Where(x => x.ProductId == y.ProductId).Select(x => x.Price).FirstOrDefault(),
                Quantity = y.Quantity
            });
            
            _dbcontext.OrderDetails.AddRange(orderDetails);
            order.TotalPrice = orderDetails.Sum(x => x.Quantity * x.ProductPrice);

            _dbcontext.SaveChanges();
            return request;
        }
    }
}
