using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using OrderMicroService.Models;


namespace OrderMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMongoCollection<Order> _orderCollection;
        public OrdersController()
        {
            var dbHost = "localhost";
            var dbPort = "27017";
            var dbName = "order_ms_db";
            string connectionString = $"mongodb://{dbHost}:{dbPort}/{dbName}";

            var mongoUrl = MongoUrl.Create(connectionString);
            var client = new MongoClient(mongoUrl);
            var database = client.GetDatabase(mongoUrl.DatabaseName);
            _orderCollection = database.GetCollection<Order>("orders");

        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var orders = await _orderCollection.Find(Builders<Order>.Filter.Empty).ToListAsync();
            return Ok(orders);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] string Id)
        {
            var filterDefinition = Builders<Order>.Filter.Eq(o => o.Id, Id);
            var order = await _orderCollection.Find(filterDefinition).SingleOrDefaultAsync();
            if (order == null)
            {
                return NotFound($"Order with Id {Id} not found.");
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> PostCreateAsync([FromBody] Order order)
        {
            await _orderCollection.InsertOneAsync(order);
            return Ok("created successfuly");
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutUpdateAsync([FromRoute] string Id, [FromBody] Order updatedOrderDetails)
        {
            var filterDefinition = Builders<Order>.Filter.Eq(o => o.Id, Id);
            var updateDefinition = Builders<Order>.Update
                .Set(o => o.OrderDetails, updatedOrderDetails.OrderDetails);
        
            var updatedOrder = await _orderCollection.FindOneAndUpdateAsync(
                filterDefinition,
                updateDefinition,
                new FindOneAndUpdateOptions<Order> { ReturnDocument = ReturnDocument.After });
        
            if (updatedOrder == null)
            {
                return NotFound($"Order with Id {Id} not found.");
            }
            return Ok(updatedOrder);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string Id)
        {
            var filterDefinition = Builders<Order>.Filter.Eq(o => o.Id, Id);
            var result = await _orderCollection.DeleteOneAsync(filterDefinition);
            if (result.DeletedCount == 0)
            {
                return NotFound($"Order with Id {Id} not found.");
            }
            return Ok("Deleted successfully");
        }
    }
}
