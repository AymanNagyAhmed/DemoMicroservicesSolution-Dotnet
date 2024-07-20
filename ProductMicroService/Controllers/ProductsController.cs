using ProductMicroService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProductMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public ProductsController(AppDbContext productDbContext)
        {
            _dbContext = productDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var products = await _dbContext.Products.ToListAsync();
            return Ok(products);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { message = $"Product {id} not found." });
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> PostCreateAsync([FromBody] Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return Ok("Created successfully.");
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutUpdateAsync([FromRoute] int id, [FromBody] Product productData)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            product.Name = productData.Name;
            product.Code = productData.Code;
            product.Price = productData.Price;
            await _dbContext.SaveChangesAsync();
            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { message = $"Product {id} not found." });
            }
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return Ok(new { message = "Product deleted successfully." });
        }
    }
}
