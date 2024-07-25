// using CustomerMicroService.Dtos;
using CustomerMicroService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _customerDbContext;
        public CustomersController(AppDbContext customerDbContext)
        {
            _customerDbContext = customerDbContext;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllAsync()
        {
            var customers = await _customerDbContext.Customers.ToListAsync();
            Object response = new
            {
                success = true,
                message = "Customers fetched successfully",
                statusCode = 200,
                companyCode = 1234,
                data = new { customers }
            };
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var customer = await _customerDbContext.Customers.FindAsync(id);
            object response;
            if (customer == null)
            {
                response = new
                {
                    success = false,
                    message = $"Customer Id {id} not found",
                    statusCode = 404,
                    companyCode = 1234,
                    error = $"Customer Id {id} not found"
                };
                return NotFound(response);
            }
            response = new
            {
                success = true,
                message = "Customer fetched successfully",
                statusCode = 200,
                companyCode = 1234,
                data = customer
            };
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostCreateAsync(Customer customer)
        {
            await _customerDbContext.Customers.AddAsync(customer);
            await _customerDbContext.SaveChangesAsync();
            Object response = new
            {
                success = true,
                message = "Created Successfully",
                statusCode = 201,
                companyCode = 1234,
                data = new { }
            };

            return Ok(response);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> PutUpdateAsync([FromRoute] int id, Customer customerData)
        {
            var customer = await _customerDbContext.Customers.FindAsync(id);
            object response;
            if (customer == null)
            {
                response = new
                {
                    success = false,
                    message = $"Customer Id {id} not found",
                    statusCode = 404,
                    companyCode = 1234,
                    error = $"Customer Id {id} not found"
                };
                return NotFound(response);
            }
            customer.Name = customerData.Name;
            customer.Mobile = customerData.Mobile;
            customer.Email = customerData.Email;
            await _customerDbContext.SaveChangesAsync();
            response = new
            {
                success = true,
                message = "Updated Successfully",
                statusCode = 200,
                companyCode = 1234,
                data = customer
            };
            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var customer = await _customerDbContext.Customers.FindAsync(id);
            Object response;
            if (customer == null)
            {
                response = new
                {
                    success = false,
                    message = $"Customer Id {id} not found",
                    statusCode = 404,
                    companyCode = 1234,
                    error = $"Customer Id {id} not found"
                };
                return NotFound(response);
            }
            _customerDbContext.Customers.Remove(customer);
            await _customerDbContext.SaveChangesAsync();
            response = new
            {
                success = true,
                message = "Deleted Successfully",
                statusCode = 200,
                companyCode = 1234,
                data = new { }
            };
            return Ok(response);
        }
    }
}
