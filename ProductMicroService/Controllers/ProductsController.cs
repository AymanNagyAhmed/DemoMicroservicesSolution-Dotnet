using ProductMicroService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductMicroService.Dtos;
using AutoMapper;
// using Microsoft.AspNetCore.Authorization;


namespace ProductMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly AppDbContext _dbContext;
        public ProductsController(AppDbContext productDbContext, IMapper mapper)
        {
            _dbContext = productDbContext;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            List<Product> products = await _dbContext.Products.ToListAsync();
            IEnumerable<ProductDetailsDto> data = _mapper.Map<IEnumerable<ProductDetailsDto>>(products);
            Object response = new
            {
                success = true,
                message = "Products retrive successfully.",
                statusCode = 200,
                companyCode = 1234,
                data
            };
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var product = await _dbContext.Products.SingleOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound(new { message = $"Product {id} not found." });
            }
            ProductDetailsDto data = _mapper.Map<ProductDetailsDto>(product);
            Object response = new
            {
                success = true,
                message = "Product retrive successfully.",
                statusCode = 200,
                companyCode = 1234,
                data
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostCreateAsync([FromBody] CreateProductDto dto)
        {
            try
            {
                Product product = new Product
                {
                    Name = dto.Name,
                    Code = dto.Code,
                    Price = dto.Price
                };
                await _dbContext.AddAsync(product);
                await _dbContext.SaveChangesAsync();
                ProductDetailsDto data = _mapper.Map<ProductDetailsDto>(product);
                Object response = new
                {
                    success = true,
                    message = "Created successfully.",
                    statusCode = 200,
                    companyCode = 1234,
                    data
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                Object errorResponse = new
                {
                    success = false,
                    message = "An error occurred while creating the product.",
                    statusCode = 500,
                    companyCode = 1234,
                    error = ex.Message
                };

                return StatusCode(500, errorResponse);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutUpdateAsync([FromRoute] int id, [FromBody] UpdateProductDto dto)
        {

            Product? product = await _dbContext.Products.SingleOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                Object errorResponse = new
                {
                    success = false,
                    message = $"Product {id} not found.",
                    statusCode = 404,
                    companyCode = 1234,
                    error = $"Product {id} not found.",
                };
                return NotFound(errorResponse);
            }

            try
            {
                product.Name = dto.Name;
                product.Code = dto.Code;
                product.Price = dto.Price;
                await _dbContext.SaveChangesAsync();
                ProductDetailsDto data = _mapper.Map<ProductDetailsDto>(product);
                Object successResponse = new
                {
                    success = true,
                    message = "Product Updated successfully.",
                    statusCode = 200,
                    companyCode = 1234,
                    data
                };

                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                Object errorResponse = new
                {
                    success = false,
                    message = "An error occurred while updating the product.",
                    statusCode = 500,
                    companyCode = 1234,
                    error = ex.Message
                };

                return StatusCode(500, errorResponse);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            Product? product = await _dbContext.Products.SingleOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                Object errorResponse = new
                {
                    success = false,
                    message = $"Product {id} not found.",
                    statusCode = 404,
                    companyCode = 1234,
                    error = $"Product {id} not found.",
                };
                return NotFound(errorResponse);
            }

            try
            {
                _dbContext.Remove(product);
                await _dbContext.SaveChangesAsync();
                Object successResponse = new
                {
                    success = true,
                    message = "Product deleted successfully.",
                    statusCode = 200,
                    companyCode = 1234,
                };

                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                Object errorResponse = new
                {
                    success = false,
                    message = "An error occurred while deleting the product.",
                    statusCode = 500,
                    companyCode = 1234,
                    error = ex.Message
                };

                return StatusCode(500, errorResponse);
            }
        }
    }
}
