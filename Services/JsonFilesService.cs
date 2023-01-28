using Microsoft.EntityFrameworkCore;
using ShopTI.Entities;
using ShopTI.IServices;
using System.Text.Json;

namespace ShopTI.Services
{
    public class JsonFilesService : IJsonFilesService
    {
        private readonly ShopDbContext _dbcontext;
        public JsonFilesService(
            ShopDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void SerializeObject()
        {
            var orders = GetOrders();

            string fileName = "Orders.json";

            string jsonString = "";

            var options = new JsonSerializerOptions { WriteIndented = true };

            foreach (var order in orders)
            {
                string newObject = JsonSerializer.Serialize(order, options);
                jsonString = string.Concat(jsonString, newObject, ",\n");
            }
            
            File.WriteAllText(fileName, jsonString);
        }

        private List<Order> GetOrders()
        {
            return _dbcontext
                .Orders
                .Include(x => x.User)
                .Include(x => x.OrderDetails)
                .ToList(); 
        }
    }
}
