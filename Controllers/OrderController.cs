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

        [HttpGet("getOrder")]
        //[Authorize] Obie role
        public ActionResult<List<Order>> GetOrders()
        {
            return _orderService.GetOrders();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderMaster(int id)
        {
            var orderDetails = await (from master in _dbContext.Set<Order>()
                                      join detail in _dbContext.Set<OrderDetail>()
                                      on master.OrderId equals detail.OrderId
                                      join product in _dbContext.Set<Product>()
                                      on detail.ProductId equals product.ProductId
                                      where master.OrderId == id

                                      select new
                                      {
                                          master.OrderId,
                                          detail.OrderDetailId,
                                          detail.ProductId,
                                          detail.Quantity,
                                          detail.ProductPrice,
                                          product.ProductName
                                      }).ToListAsync();

            var order = await (from a in _dbContext.Set<Order>()
                                     where a.OrderId == id

                                     select new
                                     {
                                         a.OrderId,
                                         a.UserId,
                                         a.PaymentMethod,
                                         a.TotalPrice,
                                         deletedOrderItemIds = "",
                                         OrderDetails = orderDetails
                                     }).FirstOrDefaultAsync();

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPut("{id}")]
        public ActionResult PutOrder(int id, Order order)
        {
            _orderService.PutOrder(id, order);
            return NoContent();
        }

        [HttpPost]
        public ActionResult<Order> PostOrder(Order order)
        {
            _orderService.CreateOrder(order);
            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _dbContext.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
