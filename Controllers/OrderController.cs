using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopTI.Entities;
using ShopTI.IServices;
using ShopTI.Models;

namespace ShopTI.Controllers
{
    [Route("api/order")]
    [ApiController]
    [EnableCors]
    public class OrderController : ControllerBase
    {
        public readonly IOrderService _orderService;
        private readonly ShopDbContext _dbContext;
        public OrderController(IOrderService orderService, ShopDbContext dbContext)
        {
            _orderService = orderService;
            _dbContext = dbContext;
    }

        [HttpGet]
        //[Authorize] Obie role
        public ActionResult<List<Order>> GetOrders()
        {
            return _orderService.GetOrders();
        }

        [HttpPost]
        public ActionResult<OrderDto> PostOrder(OrderDto order)
        {

            _orderService.CreateOrder(order);
            return Ok(new { id = order.OrderId });
        }
    }
}
