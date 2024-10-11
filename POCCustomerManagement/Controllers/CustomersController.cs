using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POCCustomerManagement.Entity;
using POCCustomerManagement.Interface;

namespace POCCustomerManagement.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class CustomersController : Controller
	{
		private readonly ICustomerService _customerService;

		public CustomersController(ICustomerService customerService)
		{
			_customerService = customerService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<CustomerEntity>>> GetCustomers()
		{
			var customers = await _customerService.GetCustomersAsync();
			return Ok(customers);
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<CustomerEntity>> GetCustomer(int id)
		{
			var customer = await _customerService.GetCustomerByIdAsync(id);
			if (customer == null)
			{
				return NotFound();
			}
			return Ok(customer);
		}

		[HttpPost]
		public async Task<ActionResult<CustomerEntity>> AddCustomer(CustomerEntity customer)
		{
			await _customerService.AddCustomerAsync(customer);
			return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCustomer(int id, CustomerEntity customer)
		{
			if (id != customer.Id)
			{
				return BadRequest();
			}

			await _customerService.UpdateCustomerAsync(customer);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCustomer(int id)
		{
			var customer = await _customerService.GetCustomerByIdAsync(id);
			if (customer == null)
			{
				return NotFound();
			}

			await _customerService.DeleteCustomerAsync(id);
			return NoContent();
		}
	}
}
